using Application.DTOs;
using FluentValidation;

namespace Application.Features.CreateResponse
{
    public class CreateMessageValidator : AbstractValidator<CreateMessageDto>
    {
        public CreateMessageValidator()
        {
            RuleFor(x => x.Text).NotNull().NotEmpty()
                .WithMessage("Message can not be empty or null");

        }

        public static bool IsValid(CreateMessageDto createMessageDto)
        {
            CreateMessageValidator validator = new();
            var validatorResult = validator.Validate(createMessageDto);
            return validatorResult.IsValid;
        }
    }
}
