using System;
using System.Threading;
using System.Threading.Tasks;

namespace ReservationSystem.Reservations.Domain.Offer
{
    public interface IOfferService
    {
        Task<Offer> GetOffer(Guid userId, long serviceId, CancellationToken cancellationToken);
    }
}
