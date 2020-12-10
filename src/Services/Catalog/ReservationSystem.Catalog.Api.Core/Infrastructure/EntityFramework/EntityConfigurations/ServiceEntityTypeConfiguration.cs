using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationSystem.Catalog.Core.Infrastructure.EntityFramework.Entities;

namespace ReservationSystem.Catalog.Core.Infrastructure.EntityFramework.EntityConfigurations
{
    public class ServiceEntityTypeConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable("Services", "Service");

            builder.HasKey(o => o.ServiceId);

            builder.Property(o => o.LongDescription)
                .IsRequired();

            builder.Property(o => o.ShortDescription)
                .IsRequired();

            builder.Property(o => o.Title)
                .IsRequired();

            builder.Property(o => o.CanBeReserved)
                .IsRequired();

            builder.Property(o => o.CurrencyCode)
                .IsRequired();

            builder.Property(o => o.InitialPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(e => e.CreateDate)
                   .HasDefaultValueSql("GETDATE()")
                   .IsRequired();

            builder.Property(e => e.CreatedBy)
                   .IsRequired();

            builder.Property(o => o.CategoryId).IsRequired();

            builder.HasOne<Category>()
                   .WithMany()
                   .IsRequired()
                   .HasForeignKey(nameof(Service.CategoryId));
        }
    }
}
