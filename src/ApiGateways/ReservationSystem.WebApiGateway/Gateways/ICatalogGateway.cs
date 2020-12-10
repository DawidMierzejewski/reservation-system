using System.Threading;
using System.Threading.Tasks;
using ReservationSystem.WebApp.ApiGateway.Contracts.Availability;
using ReservationSystem.WebApp.ApiGateway.Contracts.Catalog;
using AvailableDates = ReservationSystem.WebApp.ApiGateway.Contracts.Availability.AvailableDates;
using ServiceDetails = ReservationSystem.WebApp.ApiGateway.Contracts.Catalog.ServiceDetails;
using Services = ReservationSystem.WebApp.ApiGateway.Contracts.Catalog.Services;

namespace ReservationSystem.WebApp.ApiGateway.Gateways
{
    public interface ICatalogGateway
    {
        Task<ServiceDetails> GetServiceDetails(long serviceId, CancellationToken cancellationToken = default);

        Task<Services> GetServices(GetServicesQuery query, CancellationToken cancellationToken = default);

        Task<AvailableDates> GetAvailableDates(GetAvailableDatesQuery query, CancellationToken cancellationToken = default);

        Task<AddedCategoryId> AddCategory(AddCategoryBody command, CancellationToken cancellationToken = default);

        Task<CategoryDetails> GetCategoryDetails(long categoryId, CancellationToken cancellationToken = default);

        Task<AddedServiceId> AddService(AddServiceBody command, CancellationToken cancellationToken = default);
    }
}
