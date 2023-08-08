using FluentValidation;

namespace Application.Features.UserFeatures.Login
{
    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress()
                .WithMessage("Email can not be empty or null");

            RuleFor(x => x.Password).NotEmpty().NotNull()
                .WithMessage("Password can not be empty or null");
        }

        public static bool IsValid(LoginRequest loginRequestDto)
        {
            LoginValidator validator = new();
            var validatorResult = validator.Validate(loginRequestDto);
            return validatorResult.IsValid;
        }
    }
}
