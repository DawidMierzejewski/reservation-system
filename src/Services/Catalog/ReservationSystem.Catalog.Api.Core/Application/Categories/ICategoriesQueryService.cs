using ReservationSystem.Catalog.Api.Contracts.Category;
using System.Threading;
using System.Threading.Tasks;

namespace ReservationSystem.Catalog.Core.Application.Categories
{
    public interface ICategoriesQueryService
    {
        Task<CategoryDetails> CategoryDetails(int categoryId, CancellationToken cancellationToken = default);
    }
}
