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


            // navigation
            builder.HasOne<FAQCategory>(i => i.Category)
                .WithMany()
                .HasForeignKey(i => i.CategoryId);
        }
    }
}
