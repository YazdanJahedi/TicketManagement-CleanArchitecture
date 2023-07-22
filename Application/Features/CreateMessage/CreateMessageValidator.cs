using Application.DTOs.Message;
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
    }
}
