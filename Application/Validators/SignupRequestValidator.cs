using Application.Dtos.UserDtos;
using FluentValidation;

namespace Application.Validators
{
    public class SignupRequestValidator : AbstractValidator<SignupRequest>
    {
        public SignupRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().NotNull();
            RuleFor(x => x.PhoneNumber).NotEmpty().NotNull();
            RuleFor(x => x.Name).NotEmpty().NotNull();
        }

    }
}
