using Domain.Common;

namespace Domain.Entities
{
    public record Message : BaseEntity
    {
        public required long TicketId { get; set; }
        public required string Text { get; set; }
        public required string CreatorEmail { get; set; }
    }
}
