using Application.DTOs;
using FluentValidation;

namespace Application.Features.CreateTicket
{
    public class CreateTicketValidator : AbstractValidator<CreateTicketDto>
    {
        public CreateTicketValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty()
                .WithMessage("Title can not be empty or null");

            RuleFor(x => x.Description).NotNull().NotEmpty()
                .WithMessage("Descriptiion can not be empty or null");
        }
    }
}
