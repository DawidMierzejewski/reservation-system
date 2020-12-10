using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using ReservationSystem.Base.Services.Identity;
using ReservationSystem.Catalog.Core.Application.Categories.Commands.AddCategory;
using ReservationSystem.Catalog.Core.Infrastructure.EntityFramework;
using ReservationSystem.Catalog.Core.Infrastructure.EntityFramework.Entities;

namespace ReservationSystem.Catalog.Core.Application.Categories
{
    public class CategoriesCommandService : CommandServiceBase, ICategoriesCommandService
    {
        private readonly CatalogDbContext _dbContext;

        private readonly IIdentityContext _identityContext;

        public CategoriesCommandService(CatalogDbContext dbContext, 
            IIdentityContext identityContext,
            IValidatorFactory validationFactory) : base(validationFactory)
        {
            _dbContext = dbContext;
            _identityContext = identityContext;
        }

        public async Task<CategoryId> AddCategory(AddCategoryCommand command, CancellationToken cancellationToken = default)
        {
            ValidateCommand(command);

            var addedCategory = await _dbContext.AddAsync(new Category(
                 command.CategoryName,
                 _identityContext.UserId), cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            var categoryId = addedCategory.Entity.CategoryId;

            return new CategoryId
            {
                Value = categoryId
            };
        }
    }
}
