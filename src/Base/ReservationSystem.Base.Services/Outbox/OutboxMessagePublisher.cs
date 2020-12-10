using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReservationSystem.Base.Domain;
using ReservationSystem.Base.Services.Events;

namespace ReservationSystem.Base.Services.Outbox
{
    public class OutboxMessagePublisher : IMessagePublisher
    {
        private readonly IOutboxMessagePreparation _outbox;
        private readonly IIntegrationEventsMapper _integrationEventsMapper;

        public OutboxMessagePublisher(IOutboxMessagePreparation outbox, IIntegrationEventsMapper integrationEventsMapper)
        {
            _outbox = outbox;
            _integrationEventsMapper = integrationEventsMapper;
        }

        public async Task PublishAsync(IEnumerable<IntegrationEvent> events)
        {
            if (events == null)
            {
                throw new ArgumentNullException(nameof(events));
            }

            var integrationEvents = events.ToArray();
            if (integrationEvents.Any(e => e == null))
            {
                throw new ArgumentException($"parameter: {nameof(events)} contains null element");
            }

            foreach (var @event in integrationEvents)
            {
                await _outbox.PrepareMessageToPublishAsync(@event, @event.ObjectId);
            }
        }

        public async Task PublishAsync(IntegrationEvent @event)
        {
            await PublishAsync(new[] { @event });
        }

        public async Task PublishAsync(IEnumerable<IDomainEvent> events)
        {
            await PublishAsync(_integrationEventsMapper.MapFromDomainEvents(events).ToArray());
        }

        public async Task PublishAsync(IDomainEvent @event)
        {
            await PublishAsync(_integrationEventsMapper.MapFromDomainEvent(@event));
        }
    }
}
