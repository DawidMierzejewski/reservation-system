using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationSystem.Reservations.Domain.Service;

namespace ReservationSystem.Reservations.Infrastructure.EntityFramework.EntityConfigurations
{
    public class ServiceEntityTypeConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable("Services", "Services");

            builder.HasKey(s => s.ServiceId);

            builder.Property(s => s.CanBeReserved);
        }
    }
}
