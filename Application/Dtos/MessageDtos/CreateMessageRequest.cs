using Microsoft.AspNetCore.Http;

namespace Application.Dtos.MessageDtos
{
    public record CreateMessageRequest
    {
        public long TicketId { get; set; }
        public required string Text { get; set; }
        public IEnumerable<IFormFile>? Attacments { get; set; }
    }
}
