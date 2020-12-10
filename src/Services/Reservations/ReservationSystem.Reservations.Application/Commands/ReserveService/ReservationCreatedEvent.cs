using System;
using ReservationSystem.Base;

namespace ReservationSystem.Reservations.Application.Commands.ReserveService
{
    public class ReservationCreatedEvent : IntegrationEvent
    {
        public string ObjectId { get; }

        public long ReservationId { get; }
        public long ServiceId { get; }
        public Guid UserId { get; }

        public ReservationCreatedEvent(long reservationId, long serviceId, Guid userId)
        {
            ReservationId = reservationId;
            ObjectId = reservationId.ToString();
            ServiceId = serviceId;
            UserId = userId;
        }
    }
}
