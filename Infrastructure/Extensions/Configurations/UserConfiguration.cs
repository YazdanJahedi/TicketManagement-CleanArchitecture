using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Extensions.Configurations
{
    public class UserConfiguration : BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {

            builder.Property(e => e.Name)
                .IsRequired();

            builder.Property(e => e.Email)
                .IsRequired();

            builder.Property(e => e.Role)
                .IsRequired();

            builder.Property(e => e.PasswordHash)
                .IsRequired();

            builder.Property(e => e.PhoneNumber)
                .IsRequired();
        }
    }
}
