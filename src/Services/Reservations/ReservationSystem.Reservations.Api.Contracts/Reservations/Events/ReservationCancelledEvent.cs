using System;
using ReservationSystem.Base;

namespace ReservationSystem.Reservations.Api.Contracts.Reservations.Events
{
    public class ReservationCancelledEvent : IntegrationEvent
    {
        public string ObjectId { get; }

        public long ReservationId { get; }

        public Guid DateId { get; }

        public ReservationCancelledEvent(long reservationId, Guid dateId)
        {
            ReservationId = reservationId;
            ObjectId = reservationId.ToString();
            DateId = dateId;
        }
    }
}
