using Microsoft.EntityFrameworkCore;
using ReservationSystem.Base.Services.Outbox.EntityFramework.Entities;
using ReservationSystem.Scheduling.Domain.DailySchedule;
using ReservationSystem.Scheduling.Domain.ScheduledDates;
using ReservationSystem.Scheduling.Infrastructure.EntityFramework.EntityConfigurations;

namespace ReservationSystem.Scheduling.Infrastructure.EntityFramework
{
    public class SchedulingContext : DbContext
    {
        public SchedulingContext(DbContextOptions<SchedulingContext> options) : base(options)
        {
        }

        public SchedulingContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ScheduledDateEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OutboxMessageEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DailyScheduleEntityConfiguration());
        }

        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        public DbSet<ScheduledDate> ScheduledDates { get; set; }

        public DbSet<DailySchedule> DailySchedule { get; set; }
    }
}
