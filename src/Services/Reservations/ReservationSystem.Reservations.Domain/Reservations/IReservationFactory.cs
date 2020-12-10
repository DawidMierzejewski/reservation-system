using ReservationSystem.Reservations.Domain.AvailableDates;
using System;

namespace ReservationSystem.Reservations.Domain.Reservations
{
    public interface IReservationFactory
    {
        Reservation Create(AvailableDate availableDate, Offer.Offer offer, Guid userId, Service.Service service, DateTime now);
    }
}
