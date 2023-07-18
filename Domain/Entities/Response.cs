using Domain.Common;

namespace Domain.Entities
{
    public record Message : BaseEntity
    {
        public required long TicketId { get; set; }
        public string? Text { get; set; }
        public required long CreatorId { get; set; }
    }
}
