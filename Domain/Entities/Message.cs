using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public record Message : BaseEntity
    {
        [ForeignKey("Ticket")]
        public required long TicketId { get; set; }
        public virtual Ticket? Ticket { get; set; }

        //[ForeignKey("Creator")]
        //public required long CreatorId { get; set; }
        //public virtual User? Creator { get; set; }

        public required string Text { get; set; }

    }
}
