using System;
using ReservationSystem.Base;

namespace ReservationSystem.Reservations.Api.Contracts.Reservations.Events
{
    public class ReservationCreatedEvent : IntegrationEvent
    {
        public string ObjectId { get; }
        public long ReservationId { get; }
        public long ServiceId { get; }
        public Guid UserId { get; }
        public Guid DateId { get; }

        public ReservationCreatedEvent(long reservationId, long serviceId, Guid userId, Guid dateId)
        {
            ReservationId = reservationId;
            ObjectId = reservationId.ToString();
            ServiceId = serviceId;
            UserId = userId;
            DateId = dateId;
        }
    }
}
