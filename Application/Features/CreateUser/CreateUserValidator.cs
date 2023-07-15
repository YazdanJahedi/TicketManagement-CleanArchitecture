using FluentValidation;

namespace Application.Features.CreateUser
{
    public class CreateUserValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserValidator() 
        { 
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name can not be empty or null");
            RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage("Email can not be empty or null");
            RuleFor(x => x.Password).NotEmpty().NotNull().WithMessage("Password can not be empty or null");

        }
    }
}
