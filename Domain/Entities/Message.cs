using Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public record Message : BaseEntity
    {
        [Required]
        [ForeignKey("Ticket")]
        public required long TicketId { get; set; }
        public required string Text { get; set; }
        public required string CreatorEmail { get; set; }
    }
}
