﻿using Domain.Common;

namespace Domain.Entities
{
    public record FAQItem : BaseEntity
    {
        public required long CategoryId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }

        public virtual FAQCategory? Category { get; set; }

    }
}
