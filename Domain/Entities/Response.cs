using Domain.Common;

namespace Domain.Entities
{
    public record Response : BaseEntity
    {
        public required long TicketId { get; set; }
        public string? Writer { get; set; }
        public string? Text { get; set; }
    }
}
