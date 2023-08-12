using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions.Configurations
{
    public class FaqCategoryConfiguration : BaseEntityConfiguration<FAQCategory>
    {

        public override void Configure(EntityTypeBuilder<FAQCategory> builder)
        {
            // base.Configure(builder);

            builder.Property(e => e.CategoryName)
                .IsRequired();
        }
    }

}
