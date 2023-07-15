using Domain.Common;

namespace Domain.Entities
{
    public record Ticket : BaseEntity
    {
        public long UserId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsChecked { get; set; }
        public int NumberOfResponses { get; set; }
        public DateTime? FirstResponseDate { get; set; }
        public DateTime? CloseDate { get; set; }
    }
}
