using Domain.Common;
using Domain.Entities;

namespace Application.DTOs.Message
{
     public record MessageResponse : BaseEntity
    {
        public required long TicketId { get; set; }
        public required string Text { get; set; }
        public User? Creator { get; set; }
    }
}
