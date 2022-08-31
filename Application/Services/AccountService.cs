using Application.Dto;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository userRepository;
        private readonly IPasswordHasher<User> passwordHasher;
        private readonly AuthenticationSettings authenticationSettings;

        public AccountService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher,
            AuthenticationSettings authenticationSettings)
        {
            this.userRepository = userRepository;
            this.passwordHasher = passwordHasher;
            this.authenticationSettings = authenticationSettings;
        }

        public string GenerateJwt(LoginUserDto dto)
        {
            var user = userRepository.GetExistingUser(dto.Email);

            if (user is null)
                throw new BadRequestException("Invalid username or password");

            var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if (result == PasswordVerificationResult.Failed)
                throw new BadRequestException("Invalid username or password");

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(authenticationSettings.JwtExpireDays));

            var token = new JwtSecurityToken(authenticationSettings.JwtIssuer,
                authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        public void RegisterUser(RegisterUserDto dto)
        {
            var newUser = new User();
            newUser.FullName = dto.FullName;
            newUser.Email = dto.Email;

            var hashedPassword = passwordHasher.HashPassword(newUser, dto.Password);
            newUser.PasswordHash = hashedPassword;

            userRepository.AddNewUser(newUser);
        }
    }
}
