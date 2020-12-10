using System.Threading;
using System.Threading.Tasks;
using ReservationSystem.Catalog.Api.Contracts.Services;
using ReservationSystem.Catalog.Core.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ReservationSystem.Catalog.Core.Application.Services
{
    public class ServicesQueryService : IServicesQueryService
    {
        private readonly CatalogDbContext _dbContext;

        public ServicesQueryService(CatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Offer> GetOffer(Queries.GetOfferQuery query, CancellationToken cancellationToken = default)
        {
            var service = await _dbContext
                .Services
                .SingleAsync(o => o.ServiceId == query.ServiceId, cancellationToken);

            return new Offer
            {
                CurrencyCode = service.CurrencyCode,
                Price = service.InitialPrice
            };
        }

        public async Task<ServiceDetails> GetServiceDetails(Queries.GetServiceDetailsQuery query, CancellationToken cancellationToken = default)
        {
            var service = await _dbContext
                 .Services
                 .SingleAsync(o => o.ServiceId == query.ServiceId, cancellationToken);

            return new ServiceDetails
            {
                CurrencyCode = service.CurrencyCode,
                InitialPrice = service.InitialPrice,
                CanBeReserved = service.CanBeReserved,
                CategoryId = service.CategoryId,
                LongDescription = service.LongDescription,
                ServiceId = service.ServiceId,
                ShortDescription = service.ShortDescription,
                Title = service.Title
            };
        }

        public async Task<Api.Contracts.Services.Services> GetServices(Queries.GetServicesQuery query, CancellationToken cancellationToken = default)
        {
            var serviceItems = await _dbContext
                .Services
                .Select(s => new ServiceItem
                {
                    ServiceId = s.ServiceId,
                    ShortDescription = s.ShortDescription,
                    LongDescription = s.LongDescription,
                    InitialPrice = s.InitialPrice
                }).ToArrayAsync(cancellationToken);

            return new Api.Contracts.Services.Services
            {
                Items = serviceItems
            };
        }
    }
}