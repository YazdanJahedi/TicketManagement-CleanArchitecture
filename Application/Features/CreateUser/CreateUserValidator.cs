using Application.DTOs;
using Application.Features.LoginUser;
using FluentValidation;

namespace Application.Features.CreateUser
{
    public class CreateUserValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserValidator() 
        { 
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress()
                .WithMessage("Email can not be empty or null");

            RuleFor(x => x.Password).NotEmpty().NotNull()
                .WithMessage("Password can not be empty or null");
        }
        public static bool IsValid(CreateUserDto createUserDto)
        {
            CreateUserValidator validator = new();
            var validatorResult = validator.Validate(createUserDto);
            return validatorResult.IsValid;
        }
    }
}
