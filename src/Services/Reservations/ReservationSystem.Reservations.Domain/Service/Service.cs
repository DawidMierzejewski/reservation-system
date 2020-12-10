namespace ReservationSystem.Reservations.Domain.Service
{
    public class Service
    {
        public long ServiceId { get; }

        public bool CanBeReserved { get; }

        public Service(long serviceId, bool canBeReserved)
        {
            ServiceId = serviceId;
            CanBeReserved = canBeReserved;
        }
    }
}
