using MediatR;

namespace Application.DTOs.MessageDtos
{
    public record CreateMessageRequest : IRequest
    {
        public long TicketId { get; set; }
        public required string Text { get; set; }

    }
}
