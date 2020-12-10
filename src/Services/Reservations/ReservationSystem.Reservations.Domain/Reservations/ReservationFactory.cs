using System;
using ReservationSystem.Base.Domain.Attributes;
using ReservationSystem.Reservations.Domain.AvailableDates;
using ReservationSystem.Reservations.Domain.Reservations.Exceptions;

namespace ReservationSystem.Reservations.Domain.Reservations
{
    [DomainFactory]
    public class ReservationFactory : IReservationFactory
    {
        public Reservation Create(AvailableDate availableDate, Offer.Offer offer, Guid userId, Service.Service service, DateTime now)
        {
            if (availableDate == null)
            {
                throw new ArgumentNullException(nameof(availableDate));
            }

            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (!availableDate.IsAvailable)
            {
                throw new AvailableDateIsNotFreeException();
            }

            if (availableDate.StartDateTime < now)
            {
                throw new AvailableDateIsPastException();
            }

            if (availableDate.StartDateTime < now.AddMinutes(15))
            {
                throw new AvailableDateIsTooLateException();
            }

            return new Reservation(offer, 
                new ReservationDate(availableDate.DateId, availableDate.StartDateTime, availableDate.EndDateTime, availableDate.IsAvailable), 
                service.ServiceId, userId);
        }
    }
}
