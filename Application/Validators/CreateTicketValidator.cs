using Application.Dtos.TicketDtos;
using FluentValidation;

namespace Application.Validators
{
    public class CreateTicketValidator : AbstractValidator<CreateTicketRequest>
    {
        public CreateTicketValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty()
                .WithMessage("Title can not be empty or null");

            RuleFor(x => x.Description).NotNull().NotEmpty()
                .WithMessage("Descriptiion can not be empty or null");

            RuleFor(x => x.FaqCatgoryId).NotNull().NotEmpty();
        }
        public static bool IsValid(CreateTicketRequest createTicketDto)
        {
            CreateTicketValidator validator = new();
            var validatorResult = validator.Validate(createTicketDto);
            return validatorResult.IsValid;
        }
    }
}
