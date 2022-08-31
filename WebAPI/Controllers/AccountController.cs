using Application.Dto;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
        {
            accountService.RegisterUser(dto);

            return Ok("New user created");
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginUserDto dto)
        {
            string token = accountService.GenerateJwt(dto);

            return Ok(token);
        }
    }
}
