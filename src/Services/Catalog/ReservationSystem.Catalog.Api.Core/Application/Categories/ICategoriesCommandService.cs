using ReservationSystem.Catalog.Core.Application.Categories.Commands.AddCategory;
using System.Threading;
using System.Threading.Tasks;

namespace ReservationSystem.Catalog.Core.Application.Categories
{
    public interface ICategoriesCommandService
    {
        Task<CategoryId> AddCategory(AddCategoryCommand command, CancellationToken cancellationToken = default);
    }
}
