using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public record Ticket : BaseEntity
    {
        public required long CreatorId { get; set; }
        public required long FaqCategoryId { get; set; }
        public required string Title { get; set; }
        public required TicketStatus Status { get; set; }
        public DateTime? FirstResponseDate { get; set; }     
        public DateTime? CloseDate { get; set; }

        public virtual User? Creator { get; set; }
        public virtual FAQCategory? FaqCategory { get; set; }
        public virtual ICollection<Message>? Messages { get; set; }
    }
}
