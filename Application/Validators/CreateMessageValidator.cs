using Application.Dtos.MessageDtos;
using FluentValidation;

namespace Application.Validators
{
    public class CreateMessageValidator : AbstractValidator<CreateMessageRequest>
    {
        public CreateMessageValidator()
        {
            RuleFor(x => x.Text).NotNull().NotEmpty()
                .WithMessage("Message can not be empty or null");

            RuleFor(x => x.TicketId).NotEmpty().NotNull();

        }

        public static bool IsValid(CreateMessageRequest createMessageDto)
        {
            CreateMessageValidator validator = new();
            var validatorResult = validator.Validate(createMessageDto);
            return validatorResult.IsValid;
        }
    }
}
