using MediatR;


namespace Application.Features.TicketFeatures.CreateTicket
{
    public record CreateTicketRequest : IRequest<CreateTicketResponse>
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required long FaqCatgoryId { get; set; }
    }
}
