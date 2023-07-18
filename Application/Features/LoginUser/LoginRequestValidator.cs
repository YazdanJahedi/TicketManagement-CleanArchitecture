using Application.DTOs;
using FluentValidation;

namespace Application.Features.LoginUser
{
    public class LoginRequestValidator : AbstractValidator<LoginRequestDto>
    {
        public LoginRequestValidator() 
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress().WithMessage("Email can not be empty or null");
            RuleFor(x => x.Password).NotEmpty().NotNull().WithMessage("Password can not be empty or null");
        }
    }
}
