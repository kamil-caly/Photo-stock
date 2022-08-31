using System.ComponentModel.DataAnnotations;

namespace Application.Dto
{
    public class RegisterUserDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        public string FullName { get; set; }
    }
}
