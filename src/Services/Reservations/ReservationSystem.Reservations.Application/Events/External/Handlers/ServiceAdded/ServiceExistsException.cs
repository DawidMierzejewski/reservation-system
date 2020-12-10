using System;

namespace ReservationSystem.Reservations.Application.Events.External.Handlers.ServiceAdded
{
    public class ServiceExistsException : ApplicationException
    {
        public long ServiceId { get; }

        public ServiceExistsException(long serviceId) : base($"Service exists serviceId: {serviceId}")
        {
            ServiceId = serviceId;
        }
    }
}
