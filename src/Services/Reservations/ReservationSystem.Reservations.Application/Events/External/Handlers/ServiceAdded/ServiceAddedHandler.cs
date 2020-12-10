using System.Threading.Tasks;
using MassTransit;
using ReservationSystem.Catalog.Api.Contracts.Services.Events;
using ReservationSystem.Reservations.Domain.Service;

namespace ReservationSystem.Reservations.Application.Events.External.Handlers.ServiceAdded
{
    public class ServiceAddedHandler : IConsumer<ServiceAddedEvent>
    {
        private readonly IServicesRepository _servicesRepository;

        public ServiceAddedHandler(IServicesRepository servicesRepository)
        {
            _servicesRepository = servicesRepository;
        }

        public async Task Consume(ConsumeContext<ServiceAddedEvent> context)
        {
            var @event = context.Message;

            if ((await _servicesRepository.GetAsync(@event.ServiceId)) != null)
            {
                throw new ServiceExistsException(@event.ServiceId);
            }

            await _servicesRepository.AddAsync(new Service(@event.ServiceId, @event.CanBeReserved));
        }
    }
}
