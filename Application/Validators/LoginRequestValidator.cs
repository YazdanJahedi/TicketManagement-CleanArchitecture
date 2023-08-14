using Application.Dtos.UserDtos;
using FluentValidation;

namespace Application.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress()
                .WithMessage("Email can not be empty or null");

            RuleFor(x => x.Password).NotEmpty().NotNull()
                .WithMessage("Password can not be empty or null");
        }
    }
}
