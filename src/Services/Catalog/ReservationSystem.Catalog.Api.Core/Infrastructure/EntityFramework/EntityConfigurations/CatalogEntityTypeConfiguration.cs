using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationSystem.Catalog.Core.Infrastructure.EntityFramework.Entities;

namespace ReservationSystem.Catalog.Core.Infrastructure.EntityFramework.EntityConfigurations
{
    public class CatalogEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories", "Category");

            builder.HasKey(o => o.CategoryId);

            builder.Property(o => o.Name).IsRequired();

            builder.Property(e => e.CreateDate)
                   .HasDefaultValueSql("GETDATE()")
                   .IsRequired();

            builder.Property(e => e.CreatedBy)
                   .IsRequired();
        }
    }
}
