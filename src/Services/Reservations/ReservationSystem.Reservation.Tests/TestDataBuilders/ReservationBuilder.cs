using System;
using ReservationSystem.Base.Domain.ValueObjects.Day;
using ReservationSystem.Reservations.Domain.AvailableDates;
using ReservationSystem.Reservations.Domain.Offer;
using ReservationSystem.Reservations.Domain.Reservations;
using Currency = ReservationSystem.Base.Domain.ValueObjects.Money.Currency;

namespace ReservationSystem.Reservations.Tests.TestDataBuilders
{
    public class ReservationBuilder
    {
        private long _serviceId;

        private Guid _userId = Guid.NewGuid();

        private Offer _offer = new Offer(1.0M, Currency.PLN.ToString());

        private ReservationStatus _reservationStatus = ReservationStatus.Reserved;

        private ReservationDate _availableDate = new ReservationDate(
            Guid.NewGuid(),
            new DateTime(2020,11,20, 10, 0, 0),
            new DateTime(2020, 11, 20, 10, 15, 0), true);

        public ReservationBuilder WithOffer()
        {
            return this;
        }

        public ReservationBuilder WithReservationDateTime(DateTime startDateTime, DateTime endDateTime)
        {
            _availableDate = new ReservationDate(
                Guid.NewGuid(),
                startDateTime,
                endDateTime, true);
            return this;
        }

        public ReservationBuilder WithService(long serviceId)
        {
            _serviceId = serviceId;

            return this;
        }

        public ReservationBuilder WithUser(Guid userId)
        {
            _userId = userId;

            return this;
        }

        public ReservationBuilder WithStatus(ReservationStatus reservationStatus)
        {
            _reservationStatus = reservationStatus;

            return this;
        }

        public Reservation Build()
        {
            var reservation = new Reservation(_offer, _availableDate, _serviceId, _userId);
            reservation.SetStatus(_reservationStatus);

            return reservation;
        }

        public Reservation BuildDefault()
        {
            return new Reservation(_offer, _availableDate, 22, Guid.NewGuid());
        }
    }
}
