using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationSystem.Reservations.Domain.Reservations;

namespace ReservationSystem.Reservations.Infrastructure.EntityFramework.EntityConfigurations
{
    public class ReservationEntityTypeConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("Reservations", "Reservations");
            builder.HasKey(r => r.ReservationId);

            builder.Property(r => r.ReservationStatus)
                .IsRequired();

            builder.Property(r => r.ServiceId)
                .IsRequired();

            builder.Property(r => r.UserId)
                .IsRequired();

            builder.OwnsOne(r => r.Offer, o =>
            {
                o.Property(p => p.CurrencyCode).HasColumnName("CurrencyCode").IsRequired();
                o.Property(p => p.Price).HasColumnName("Price").IsRequired();
            });

            builder.OwnsOne(r => r.ReservationDate, availableDate =>
            {
                availableDate.WithOwner().HasForeignKey(nameof(Reservation.ReservationId));

                availableDate.ToTable("ReservationDates", "Reservations");

                availableDate.Property(p => p.DateId).HasColumnName("DateId").IsRequired();

                availableDate.Property(p => p.IsAvailable).HasColumnName("IsAvailable").IsRequired();

                availableDate.Property(p => p.StartDateTime).HasColumnName("StartDateTime").IsRequired();

                availableDate.Property(p => p.EndDateTime).HasColumnName("EndDateTime").IsRequired();
            });

            builder.Property(r => r.CreatedDate)
                   .HasDefaultValueSql("GETDATE()")
                   .IsRequired();

            builder
                .Property(p => p.Timestamp)
                .IsRowVersion()
                .IsRequired();
        }
    }
}
