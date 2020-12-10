namespace ReservationSystem.Reservations.Application.Commands.ReserveService.Exceptions
{
    public class ServiceDoesNotExistException : Base.Services.Exceptions.ApplicationException
    {
        public override string Code => "service_does_not_exists";

        public long ServiceId { get; }

        public ServiceDoesNotExistException(long serviceId) : base($"Service does not exist serviceId {serviceId}")
        {
            ServiceId = serviceId;
        }
    }
}
