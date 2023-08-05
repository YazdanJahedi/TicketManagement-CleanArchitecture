using MediatR;

namespace Application.Features.MessageFeatures.CreateMessage
{
    public record CreateMessageRequest : IRequest
    {
        public long TicketId { get; set; }
        public required string Text { get; set; }

    }
}
