using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Extensions.Configurations
{
    public class BaseEntityConfiguration<TBase> : IEntityTypeConfiguration<TBase>  
        where TBase : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TBase> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(e => e.CreationDate)
                .IsRequired();
        }
    }
}
