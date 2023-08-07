using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.MessageFeatures.CreateMessage
{
    public record CreateMessageRequest : IRequest
    {
        public long TicketId { get; set; }
        public required string Text { get; set; }  
        public IEnumerable<IFormFile>? Attacments { get; set; }
    }
}
