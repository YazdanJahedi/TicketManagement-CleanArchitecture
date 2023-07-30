using Application.DTOs.UserDtos;
using FluentValidation;

namespace Application.Features.UserFeatures.Queries.Login
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

        public static bool IsValid(LoginRequest loginRequestDto)
        {
            LoginRequestValidator validator = new();
            var validatorResult = validator.Validate(loginRequestDto);
            return validatorResult.IsValid;
        }
    }
}
