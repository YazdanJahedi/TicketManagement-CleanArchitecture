using Domain.Entities;
using Infrastructure.Extensions;
using Infrastructure.Extensions.Configurations;
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

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TicketConfiguration());
            modelBuilder.ApplyConfiguration(new MessageConfiguration());
            modelBuilder.ApplyConfiguration(new MessageAttachmentConfiguration());
            modelBuilder.ApplyConfiguration(new FaqItemConfiguration());
            modelBuilder.ApplyConfiguration(new FaqCategoryConfiguration());

            modelBuilder.Seed();
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Ticket> Tickets { get; set; } = null!;
        public DbSet<FAQCategory> FAQCategories { get; set; } = null!;
        public DbSet<FAQItem> FAQItems { get; set; } = null!;
        public DbSet<Message> Messages { get; set; } = null!;
        public DbSet<MessageAttachment> MessageAttachments { get; set; } = null!;
    }
}
