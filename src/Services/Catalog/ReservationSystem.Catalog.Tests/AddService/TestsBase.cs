using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using ReservationSystem.Catalog.Core.Infrastructure.EntityFramework;
using ReservationSystem.Catalog.Core.Infrastructure.EntityFramework.Entities;

namespace ReservationSystem.Catalog.Tests.AddService
{
    public class TestsBase
    {
        protected CatalogDbContext DbContext;

        protected void ConfigureDbContextInMemmory()
        {
            var serviceProvider = new ServiceCollection()
                           .AddEntityFrameworkInMemoryDatabase()
                           .BuildServiceProvider();

            var dbContextOptions = new DbContextOptionsBuilder<CatalogDbContext>()
                .UseInMemoryDatabase(databaseName: nameof(CatalogDbContext))
                .UseInternalServiceProvider(serviceProvider)
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            DbContext = new CatalogDbContext(dbContextOptions);
        }

        protected async Task<Category> AddCategoryAsync(Category category)
        {
            var result = await DbContext.Categories.AddAsync(category);
            await DbContext.SaveChangesAsync();

            return result.Entity;
        }
    }
}
