using FluentValidation;

namespace Application.Features.CreateResponse
{
    public class CreateResponseValidator : AbstractValidator<CreateResponseRequest>
    {
        public CreateResponseValidator()
        {
            RuleFor(x => x.Text).NotNull().NotEmpty().WithMessage("message can not be empty or null");
        }
    }
}
