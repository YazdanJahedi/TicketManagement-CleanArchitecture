using Application.DTOs;
using FluentValidation;

namespace Application.Features.CreateUser
{
    public class CreateUserValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserValidator() 
        { 
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name can not be empty or null");
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress().WithMessage("Email can not be empty or null");
            RuleFor(x => x.Password).NotEmpty().NotNull().WithMessage("Password can not be empty or null");

        }
    }
}
