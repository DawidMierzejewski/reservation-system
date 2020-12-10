using System;
using System.Threading;
using System.Threading.Tasks;
using ReservationSystem.Base.Domain.Attributes;
using ReservationSystem.Catalog.Api.Contracts;
using ReservationSystem.Reservations.Domain.Offer;

namespace ReservationSystem.Reservations.Infrastructure.Services
{
    [Service]
    public class OfferService : IOfferService
    {
        private readonly ICatalogApiClient _catalogApiClient;

        public OfferService(ICatalogApiClient catalogApiClient)
        {
            _catalogApiClient = catalogApiClient;
        }

        public async Task<Offer> GetOffer(Guid userId, long serviceId, CancellationToken cancellationToken)
        {
            var offer = await _catalogApiClient.GetOffer(serviceId, cancellationToken);

            return new Offer(offer.Price, offer.CurrencyCode);
        }
    }
}
