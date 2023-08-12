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
    public class TicketConfiguration : BaseEntityConfiguration<Ticket>
    {
        public override void Configure(EntityTypeBuilder<Ticket> builder)
        {
            //base.Configure(builder);


            // navigation        
            builder.HasOne<FAQCategory>(t => t.FaqCategory)
                .WithMany()
                .HasForeignKey(t => t.FaqCategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<User>(t => t.Creator)
                .WithMany(u => u.Tickets)
                .HasForeignKey(t => t.CreatorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
