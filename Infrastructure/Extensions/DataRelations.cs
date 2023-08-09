using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class DataRelations
    {
        public static void ConfigureDataRelations(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>()
                .HasOne<User>(t => t.Creator)
                .WithMany(u => u.Tickets)
                .HasForeignKey(t => t.CreatorId);

            modelBuilder.Entity<Message>()
                .HasOne<Ticket>(m => m.Ticket)
                .WithMany(t => t.Messages)
                .HasForeignKey(m => m.TicketId);

            modelBuilder.Entity<FAQItem>()
                .HasOne<FAQCategory>(i => i.Category)
                .WithMany()
                .HasForeignKey(i => i.CategoryId);

            modelBuilder.Entity<Message>()
                .HasOne<User>(m => m.Creator)
                .WithMany()
                .HasForeignKey(m => m.CreatorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Ticket>()
                .HasOne<FAQCategory>(t => t.FaqCategory)
                .WithMany()
                .HasForeignKey(t => t.FaqCategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MessageAttachment>()
                .HasOne<Message>(a => a.Message)
                .WithMany(m => m.Attachments)
                .HasForeignKey(a => a.MessageId);
        }
    }
}
