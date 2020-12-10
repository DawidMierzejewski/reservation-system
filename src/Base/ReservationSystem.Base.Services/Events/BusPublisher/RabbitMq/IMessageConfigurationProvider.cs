using System;

namespace ReservationSystem.Base.Services.Events.BusPublisher.RabbitMq
{
    public interface IMessageConfigurationProvider
    {
        IMessageConfiguration Get(Type type);
    }
}
