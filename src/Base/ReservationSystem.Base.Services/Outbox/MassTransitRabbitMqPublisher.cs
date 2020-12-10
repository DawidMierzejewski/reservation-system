using MassTransit;
using System.Threading;
using System.Threading.Tasks;

namespace ReservationSystem.Base.Services.Outbox
{
    public class MassTransitRabbitMqPublisher : IBusPublisher
    {
        readonly IPublishEndpoint _publishEndpoint;

        public MassTransitRabbitMqPublisher(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task PublishAsync(object message, CancellationToken cancellationToken = default)
        {
            await _publishEndpoint.Publish(message, cancellationToken);
        }

        public async Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class
        {
            await _publishEndpoint.Publish(message, cancellationToken);
        }
    }
}
