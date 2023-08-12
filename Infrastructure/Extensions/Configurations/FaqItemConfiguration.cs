using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions.Configurations
{
    public class FaqItemConfiguration : BaseEntityConfiguration<FAQItem>
    {
        public override void Configure(EntityTypeBuilder<FAQItem> builder)
        {
            // base.Configure(builder);

            builder.Property(e => e.CategoryId)
                .IsRequired();

            builder.Property(e => e.Title)
                .IsRequired();

            builder.Property(e => e.Description)
                .IsRequired();

            // navigation
            builder.HasOne(i => i.Category)
                .WithMany()
                .HasForeignKey(i => i.CategoryId);
        }
    }
}
