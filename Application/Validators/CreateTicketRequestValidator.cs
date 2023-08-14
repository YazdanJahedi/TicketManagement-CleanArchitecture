using Application.Dtos.TicketDtos;
using FluentValidation;

namespace Application.Validators
{
    public class CreateTicketRequestValidator : AbstractValidator<CreateTicketRequest>
    {
        public CreateTicketRequestValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty()
                .WithMessage("Title can not be empty or null");

            RuleFor(x => x.Description).NotNull().NotEmpty()
                .WithMessage("Descriptiion can not be empty or null");

            RuleFor(x => x.FaqCatgoryId).NotNull().NotEmpty();
        }
        public static bool IsValid(CreateTicketRequest createTicketDto)
        {
            CreateTicketRequestValidator validator = new();
            var validatorResult = validator.Validate(createTicketDto);
            return validatorResult.IsValid;
        }
    }
}
