using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationSystem.Scheduling.Domain.DailySchedule;
using ReservationSystem.Scheduling.Domain.ScheduledDates;

namespace ReservationSystem.Scheduling.Infrastructure.EntityFramework.EntityConfigurations
{
    public class DailyScheduleEntityConfiguration : IEntityTypeConfiguration<DailySchedule>
    {
        public void Configure(EntityTypeBuilder<DailySchedule> builder)
        {
            builder.ToTable( "DailySchedules", "Scheduling");

            builder.HasKey(d => d.ScheduleId);

            builder
                .Property(d => d.ServiceId).IsRequired();

            builder.OwnsOne(r => r.Day, o =>
            {
                o.Property(p => p.Day).HasColumnName("Day").IsRequired();
                o.Property(p => p.Month).HasColumnName("Month").IsRequired();
                o.Property(p => p.Year).HasColumnName("Year").IsRequired();
            });

            builder.HasMany(b => b.Dates)
                .WithOne()
                .HasForeignKey(nameof(ScheduledDate.ScheduleId))
                .OnDelete(DeleteBehavior.Cascade);

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
