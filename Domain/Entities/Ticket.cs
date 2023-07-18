using Domain.Common;

namespace Domain.Entities
{
    public record Ticket : BaseEntity
    {
        public required long CreatorId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateTime? FirstResponseDate { get; set; }
        public DateTime? CloseDate { get; set; }
    }
}
