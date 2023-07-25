using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions
{
    public static class DataSeeder
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.SeedUsers();
            modelBuilder.SeedFaqCatetories();
            modelBuilder.SeedFaqItems();
        }

        private static void SeedUsers(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                    new User
                    {
                        Id = 1,
                        Email = "admin@admin.com",
                        PasswordHash = "$2a$11$.64fLerPDfuVgkHnbF3o6uBF1MGQqfxYoPivqq8HkwvevmKIbT5gy", // 1234
                        Role = "Admin",
                    }
                );
        }

        private static void SeedFaqCatetories(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FAQCategory>().HasData(
                    new FAQCategory
                    {
                        Id = 1,
                        CategoryName = "Payment",
                    },
                    new FAQCategory
                    {
                        Id = 2,
                        CategoryName = "Factor",
                    },
                    new FAQCategory
                    {
                        Id = 3,
                        CategoryName = "Others",
                    }
                );
        }

        private static void SeedFaqItems(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FAQItem>().HasData(
                    new FAQItem
                    {
                        Id = 1,
                        CategoryId = 1,
                        Title = "payment qestion 1",
                        Description = "description for peyment 1",
                    },
                    new FAQItem
                    {
                        Id = 2,
                        CategoryId = 1,
                        Title = "payment qestion 2",
                        Description = "description for peyment 2",
                    },
                    new FAQItem
                    {
                        Id = 3,
                        CategoryId = 2,
                        Title = "factor qestion 1",
                        Description = "description for factor 1",
                    },
                    new FAQItem
                    {
                        Id = 4,
                        CategoryId = 3,
                        Title = "Others qestion 1",
                        Description = "description for Others 1",
                    }
                );
        }


    }


}
