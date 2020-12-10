﻿using System.Threading;
using System.Threading.Tasks;

namespace ReservationSystem.Base.Services.Events.BusPublisher
{
    public interface IBusPublisher
    {
        Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class;

        Task PublishAsync(object message, CancellationToken cancellationToken = default);
    }
}
