using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions.Configurations
{
    public class MessageConfiguration : BaseEntityConfiguration<Message>
    {
        public override void Configure(EntityTypeBuilder<Message> builder)
        {
            // base.Configure(builder);



            // navigation
            builder.HasOne(m => m.Ticket)
                .WithMany(t => t.Messages)
                .HasForeignKey(m => m.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            
            builder.HasOne(m => m.Creator)
                .WithMany()
                .HasForeignKey(m => m.CreatorId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
