using ReservationSystem.Base;

namespace ReservationSystem.Catalog.Api.Contracts.Services.Events
{
    public class ServiceAddedEvent : IntegrationEvent
    {
        public long ServiceId { get; set; }

        public bool CanBeReserved { get; set; }

        public string ObjectId => ServiceId.ToString();

        public ServiceAddedEvent(long serviceId, bool canBeReserved)
        {
            ServiceId = serviceId;
            CanBeReserved = canBeReserved;
        }
    }
}
