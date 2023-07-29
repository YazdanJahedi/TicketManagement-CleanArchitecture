using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public record Ticket : BaseEntity
    {
        [ForeignKey("Creator")]
        public required long CreatorId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateTime? FirstResponseDate { get; set; }
        public DateTime? CloseDate { get; set; }

        // FAQ categori id

        public virtual User Creator { get; set; } // = null!;
        public virtual ICollection<Message>? Messages { get; set; }
    }
}
