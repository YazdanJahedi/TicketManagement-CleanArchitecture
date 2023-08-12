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
    public class MessageAttachmentConfiguration : BaseEntityConfiguration<MessageAttachment>
    {
        public override void Configure(EntityTypeBuilder<MessageAttachment> builder)
        {
            // base.Configure(builder);
            
            builder.Property(e => e.MessageId)
                .IsRequired();

            builder.Property(e => e.FileName)
                .IsRequired();

            // navigation
            
            builder.HasOne(a => a.Message)
                .WithMany(m => m.Attachments)
                .HasForeignKey(a => a.MessageId);
        }
    }
}
