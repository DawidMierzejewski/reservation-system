using System;
using ReservationSystem.Base.Domain;
using ReservationSystem.Reservations.Domain.AvailableDates;
using ReservationSystem.Reservations.Domain.Reservations.Events;
using ReservationSystem.Reservations.Domain.Reservations.Exceptions;

namespace ReservationSystem.Reservations.Domain.Reservations
{
    public class Reservation : Aggregate
    {
        public long ReservationId { get; }

        public ReservationStatus ReservationStatus { get; private set; }

        public long ServiceId { get; }

        public Guid UserId { get; }

        public Offer.Offer Offer { get; }

        public ReservationDate ReservationDate { get; }

        public DateTime CreatedDate { get; set; }

        protected Reservation()
        {

        }

        public Reservation(Offer.Offer offer, ReservationDate reservationDate, long serviceId, Guid userId)
        {
            Offer = offer;
            ReservationDate = reservationDate;
            UserId = userId;
            ServiceId = serviceId;
            ReservationStatus = ReservationStatus.Reserved;

            ApplyChanges(new ReservationCreatedEvent(this));
        }

        public void Cancel(DateTime now)
        {
            if (!CanCancel(now))
            {
                throw new ReservationCantBeCanceledException(ReservationId);
            }

            ReservationStatus = ReservationStatus.Canceled;

            ApplyChanges(new ReservationCancelledEvent(this));
        }

        private bool CanCancel(DateTime now)
        {
            if (ReservationStatus == ReservationStatus.Canceled)
            {
                return false;
            }

            if (now >= ReservationDate.StartDateTime.AddMinutes(-15))
            {
                return false;
            }

            return true;
        }

        public void SetStatus(ReservationStatus reservationStatus)
        {
            ReservationStatus = reservationStatus;
        }
    }
}
