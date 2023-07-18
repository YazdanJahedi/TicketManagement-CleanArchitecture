﻿using Domain.Common;

namespace Domain.Entities
{
    public record FAQItem : BaseEntity
    {
        public required long CategoryId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
