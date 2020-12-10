using System.Collections.Generic;
using ReservationSystem.Base;
using ReservationSystem.Base.Domain;
using ReservationSystem.Base.Services.Events;
using ReservationSystem.Scheduling.Domain.DailySchedule.Events;

namespace ReservationSystem.Scheduling.Infrastructure.Events
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

        private IntegrationEvent Map(DailyScheduleConfiguredEvent @event)
        {
            return new ReservationSystem.Scheduling.Api.Contracts.DailySchedule.Events.DailyScheduleConfiguredEvent(
                @event.DailySchedule.ScheduleId);
        }
    }
}
