using Domain.Common;

namespace Application.DTOs.Ticket
{
    public record TicketResponse : BaseEntity
    {
        public required long CreatorId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public bool? IsChecked { get; set; }
        public DateTime? FirstResponseDate { get; set; }
        public DateTime? CloseDate { get; set; }
    }
}
