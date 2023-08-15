using Domain.Common;

namespace Domain.Entities
{
    public record Message : BaseEntity
    {
        public required long TicketId { get; set; }
        public required long CreatorId { get; set; }
        public required string Text { get; set; }

        public virtual Ticket? Ticket { get; set; }
        public virtual User? Creator { get; set; }
        public virtual IEnumerable<MessageAttachment>? Attachments { get; set; }
    }
}
