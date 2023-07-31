using Domain.Entities;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var connectionString = "Server=(localdb)\\mssqllocaldb;Database=TicketManagementDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true;";
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
                .WithMany(c => c.Items)
                .HasForeignKey(i => i.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Message>()
                .HasOne<User>(m => m.Creator)
                .WithMany()
                .HasForeignKey(m => m.CreatorId)
                .OnDelete(DeleteBehavior.NoAction);
                

            modelBuilder.Entity<Ticket>()
                .HasOne<FAQCategory>(t => t.FaqCategory)
                .WithMany()
                .HasForeignKey(t => t.FaqCategoryId);

            modelBuilder.Seed();
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Ticket> Tickets { get; set; } = null!;
        public DbSet<FAQCategory> FAQCategories { get; set; } = null!;
        public DbSet<FAQItem> FAQItems { get; set; } = null!;
        public DbSet<Message> Messages { get; set; } = null!;

    }
}
