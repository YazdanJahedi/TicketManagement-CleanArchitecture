using Application.Dtos.MessageDtos;
using FluentValidation;

namespace Application.Validators
{
    public class CreateMessageRequestValidator : AbstractValidator<CreateMessageRequest>
    {
        public CreateMessageRequestValidator()
        {
            RuleFor(x => x.Text).NotNull().NotEmpty()
                .WithMessage("Message can not be empty or null");

            RuleFor(x => x.TicketId).NotEmpty().NotNull();

        }

        public static bool IsValid(CreateMessageRequest createMessageDto)
        {
            CreateMessageRequestValidator validator = new();
            var validatorResult = validator.Validate(createMessageDto);
            return validatorResult.IsValid;
        }
    }
}
