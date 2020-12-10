using System.Threading;
using System.Threading.Tasks;
using ReservationSystem.Catalog.Api.Contracts.Category;
using ReservationSystem.Catalog.Core.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace ReservationSystem.Catalog.Core.Application.Categories
{
    public class CategoriesQueryService : ICategoriesQueryService
    {
        private readonly CatalogDbContext _dbContext;

        public CategoriesQueryService(CatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CategoryDetails> CategoryDetails(int categoryId, CancellationToken cancellationToken = default)
        {
            var category = await _dbContext.Categories
                .SingleOrDefaultAsync(c => c.CategoryId == categoryId, cancellationToken);

            return new CategoryDetails
            {
                CategoryId = category.CategoryId,
                Name = category.Name
            };
        }
    }
}
