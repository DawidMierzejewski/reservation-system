using System.Threading;
using System.Threading.Tasks;
using Refit;
using ReservationSystem.Catalog.Api.Contracts.Category;
using ReservationSystem.Catalog.Api.Contracts.Services;

namespace ReservationSystem.Catalog.Api.Contracts
{
    public interface ICatalogApiClient
    {
        [Get("/api/services")]
        Task<Services.Services> GetServices([Query] GetServicesQuery query, CancellationToken cancellationToken);

        [Get("/api/services/{serviceId}")]
        Task<ServiceDetails> GetServiceDetails([AliasAs("serviceId")] long serviceId, CancellationToken cancellationToken);

        [Get("/api/services/{serviceId}/offer")]
        Task<Offer> GetOffer([AliasAs("serviceId")] long serviceId, CancellationToken cancellationToken);

        [Post("/api/services")]
        Task<AddedServiceId> AddService([Body] AddServiceBody addServiceBody, CancellationToken cancellationToken);

        [Post("/api/categories")]
        Task<AddedCategoryId> AddCategory([Body] AddCategoryBody addCategoryBody, CancellationToken cancellationToken);

        [Post("/api/categories/{categoryId}")]
        Task<CategoryDetails> GetCategoryDetails([AliasAs("categoryId")] long categoryId, CancellationToken cancellationToken);
    }
}
