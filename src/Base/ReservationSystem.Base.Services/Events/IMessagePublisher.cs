using System.Collections.Generic;
using System.Threading.Tasks;
using ReservationSystem.Base.Domain;

namespace ReservationSystem.Base.Services.Events
{
    public interface IMessagePublisher
    {
        public Task PublishAsync(IEnumerable<IntegrationEvent> events);

        public Task PublishAsync(IEnumerable<IDomainEvent> events);

        public Task PublishAsync(IntegrationEvent @event);

        public Task PublishAsync(IDomainEvent @event);
    }
}
