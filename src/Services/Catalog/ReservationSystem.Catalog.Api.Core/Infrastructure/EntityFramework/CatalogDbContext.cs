using Microsoft.EntityFrameworkCore;
using ReservationSystem.Base.Services.Outbox.EntityFramework.Entities;
using ReservationSystem.Catalog.Core.Infrastructure.EntityFramework.Entities;
using ReservationSystem.Catalog.Core.Infrastructure.EntityFramework.EntityConfigurations;

namespace ReservationSystem.Catalog.Core.Infrastructure.EntityFramework
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {

        }

        public CatalogDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CatalogEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OutboxMessageEntityTypeConfiguration());
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<OutboxMessage> OutboxMessages { get; set; }
    }
}
