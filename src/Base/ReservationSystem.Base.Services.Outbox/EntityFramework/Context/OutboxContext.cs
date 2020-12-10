using Microsoft.EntityFrameworkCore;
using ReservationSystem.Base.Services.Outbox.EntityFramework.Entities;

namespace ReservationSystem.Base.Services.Outbox.EntityFramework.Context
{
    public class OutboxContext : DbContext
    {
        public OutboxContext(DbContextOptions<OutboxContext> options) : base(options)
        {
        }

        public OutboxContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OutboxMessageEntityTypeConfiguration());
        }

        public DbSet<OutboxMessage> OutboxMessages { get; set; }
    }
}
