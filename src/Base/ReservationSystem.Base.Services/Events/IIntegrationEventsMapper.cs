using System.Collections.Generic;
using ReservationSystem.Base.Domain;

namespace ReservationSystem.Base.Services.Events
{
    public interface IIntegrationEventsMapper
    {
        IEnumerable<IntegrationEvent> MapFromDomainEvents(IEnumerable<IDomainEvent> domainEvents);

        IntegrationEvent MapFromDomainEvent(IDomainEvent domainEvent);
    }
}
