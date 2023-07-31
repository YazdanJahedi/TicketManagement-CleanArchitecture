using Application.DTOs.UserDtos;
using FluentValidation;

namespace Application.Features.UserFeatures.Commands.Signup
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
        public static bool IsValid(SignupRequest createUserDto)
        {
            SignupRequestValidator validator = new();
            var validatorResult = validator.Validate(createUserDto);
            return validatorResult.IsValid;
        }
    }
}
