using Domain.Common;
using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public record Ticket : BaseEntity
    {
        [ForeignKey("Creator")]
        public required long CreatorId { get; set; }
        public virtual User? Creator { get; set; }

        [ForeignKey("FAQCategory")]
        public required long FaqCategoryId { get; set; }
        public virtual FAQCategory? FaqCategory { get; set; }

        public required string Title { get; set; }

        public required TicketStatus Status { get; set; }
        public DateTime? FirstResponseDate { get; set; }     
        public DateTime? CloseDate { get; set; }

        public virtual ICollection<Message>? Messages { get; set; }
    }
}
