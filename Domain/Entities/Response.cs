using Domain.Common;

namespace Domain.Entities
{
    public record Response : BaseEntity
    {
        public long TicketId { get; set; }
        public long IdInTicket { get; set; }
        public string? Writer { get; set; }
        public string? Text { get; set; }
    }
}
