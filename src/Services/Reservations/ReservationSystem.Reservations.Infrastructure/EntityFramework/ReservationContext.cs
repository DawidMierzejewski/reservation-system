using Microsoft.EntityFrameworkCore;
using ReservationSystem.Base.Services.Outbox.EntityFramework.Entities;
using ReservationSystem.Reservations.Domain.Reservations;
using ReservationSystem.Reservations.Infrastructure.EntityFramework.EntityConfigurations;

namespace ReservationSystem.Reservations.Infrastructure.EntityFramework
{
    public class ReservationContext : DbContext
    {
        public ReservationContext(DbContextOptions<ReservationContext> options) : base(options)
        {
        }

        public ReservationContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ReservationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OutboxMessageEntityTypeConfiguration());
        }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Domain.Service.Service> Services { get; set; }

        public DbSet<OutboxMessage> OutboxMessages { get; set; }
    }
}
