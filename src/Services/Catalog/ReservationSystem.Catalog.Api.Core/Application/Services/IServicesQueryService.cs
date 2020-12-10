using System.Threading;
using System.Threading.Tasks;
using ReservationSystem.Catalog.Api.Contracts.Services;

namespace ReservationSystem.Catalog.Core.Application.Services
{
    public interface IServicesQueryService
    {
        Task<ServiceDetails> GetServiceDetails(Queries.GetServiceDetailsQuery query, CancellationToken cancellationToken = default);

        Task<Api.Contracts.Services.Services> GetServices(Queries.GetServicesQuery query, CancellationToken cancellationToken = default);

        Task<Offer> GetOffer(Queries.GetOfferQuery query, CancellationToken cancellationToken = default);
    }
}
