using Domain.Common;

namespace Domain.Entities
{
    public record FAQCategory : BaseEntity
    {
        public required string CategoryName { get; set; }

        public virtual ICollection<FAQItem>? Items { get; set; }

    }
}
