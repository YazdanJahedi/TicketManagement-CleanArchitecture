﻿using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public record Ticket : BaseEntity
    {
        [ForeignKey("Creator")]
        public required long CreatorId { get; set; }
        public virtual User? Creator { get; set; }

        [ForeignKey("FAQCategory")]
        public required long FaqCategoryId { get; set; }
        public virtual FAQCategory? FaqCategory { get; set; }

        public required string Title { get; set; }
        public DateTime? FirstResponseDate { get; set; }
        
        // close date
        public virtual ICollection<Message>? Messages { get; set; }
    }
}
