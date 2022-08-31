using Application.Dto;

namespace Application.Interfaces
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
        string GenerateJwt(LoginUserDto dto);
    }
}
