using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
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

            builder.Property(e => e.CreatorId)
                .IsRequired();

            builder.Property(e => e.FaqCategoryId)
                .IsRequired();

            builder.Property(e => e.Title)
                .IsRequired();

            // navigation
            
            builder.HasOne(t => t.FaqCategory)
                .WithMany()
                .HasForeignKey(t => t.FaqCategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(t => t.Creator)
                .WithMany()
                .HasForeignKey(t => t.CreatorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
