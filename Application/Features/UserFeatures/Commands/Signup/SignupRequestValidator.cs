using Application.DTOs.UserDtos;
using FluentValidation;

namespace Application.Features.UserFeatures.Commands.Signup
{
    public class SignupRequestValidator : AbstractValidator<SignupRequest>
    {
        public SignupRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress()
                .WithMessage("Email can not be empty or null");

            RuleFor(x => x.Password).NotEmpty().NotNull()
                .WithMessage("Password can not be empty or null");
        }
        public static bool IsValid(SignupRequest createUserDto)
        {
            SignupRequestValidator validator = new();
            var validatorResult = validator.Validate(createUserDto);
            return validatorResult.IsValid;
        }
    }
}
