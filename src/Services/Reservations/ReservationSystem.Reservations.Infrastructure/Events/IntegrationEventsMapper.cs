using System.Collections.Generic;
using ReservationSystem.Base;
using ReservationSystem.Base.Domain;
using ReservationSystem.Base.Services.Events;
using ReservationSystem.Reservations.Domain.Reservations.Events;

namespace ReservationSystem.Reservations.Infrastructure.Events
{
    public class IntegrationEventsMapper : IIntegrationEventsMapper
    {
        public IEnumerable<IntegrationEvent> MapFromDomainEvents(IEnumerable<IDomainEvent> domainEvents)
        {
            var result = new List<IntegrationEvent>();

            foreach (var domain in domainEvents)
            {
                result.Add(Map((dynamic)domain));
            }

            return result;
        }

        public IntegrationEvent MapFromDomainEvent(IDomainEvent domainEvent)
        {
            return Map((dynamic) domainEvent);
        }

        private IntegrationEvent Map(ReservationCancelledEvent @event)
        {
            return new Api.Contracts.Reservations.Events.ReservationCancelledEvent(
                @event.Reservation.ReservationId,
                @event.Reservation.ReservationDate.DateId);
        }

        private IntegrationEvent Map(ReservationCreatedEvent @event)
        {
            return new Api.Contracts.Reservations.Events.ReservationCreatedEvent(
                @event.Reservation.ReservationId,
                @event.Reservation.ServiceId,
                @event.Reservation.UserId,
                @event.Reservation.ReservationDate.DateId);
        }
    }
}
