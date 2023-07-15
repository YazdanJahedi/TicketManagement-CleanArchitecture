using Domain.Common;

namespace Domain.Entities
{
    public record FAQCategory : BaseEntity
    {
        public string? Title { get; set; }
    }
}
