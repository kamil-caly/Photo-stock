using Application.Dto;
using Domain.Interfaces;
using FluentValidation;

namespace Application.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserValidator(IUserRepository userRepository)
        {
            RuleFor(u => u.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(u => u.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = userRepository.IsEmailTaken(value.ToString());

                    if (emailInUse)
                    {
                        context.AddFailure("This Email is taken.");
                    }
                });
        }
    }
}
