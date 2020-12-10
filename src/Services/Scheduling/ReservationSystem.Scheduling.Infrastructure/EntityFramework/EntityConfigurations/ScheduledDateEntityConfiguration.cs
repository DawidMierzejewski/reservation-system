using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationSystem.Scheduling.Domain.DailySchedule;
using ReservationSystem.Scheduling.Domain.ScheduledDates;

namespace ReservationSystem.Scheduling.Infrastructure.EntityFramework.EntityConfigurations
{
    public class ScheduledDateEntityConfiguration : IEntityTypeConfiguration<ScheduledDate>
    {
        public void Configure(EntityTypeBuilder<ScheduledDate> builder)
        {
            builder.ToTable("ScheduledDates", "Scheduling");

            builder.HasKey(s => s.DateId);

            builder.OwnsOne(r => r.TimeSlot, o =>
            {
                o.Property(p => p.EndTime).HasColumnName("EndTime").IsRequired();
                o.Property(p => p.StartTime).HasColumnName("StartTime").IsRequired();
            });

            builder.HasOne<DailySchedule>()
                .WithMany()
                .IsRequired()
                .HasForeignKey(nameof(ScheduledDate.ScheduleId));

            builder.Property(s => s.StartDateTime).IsRequired();

            builder.Property(s => s.EndDateTime).IsRequired();

            builder.Property(s => s.DateId).IsRequired();

            builder.Property(s => s.Status).IsRequired();

            builder.Property(r => r.CreatedDate)
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();
        }
    }
}
