namespace ReservationSystem.Catalog.Core.Application.Services.Queries
{
    public class GetOfferQuery
    {
        public long ServiceId { get; }

        public GetOfferQuery(long serviceId) => (ServiceId) = (serviceId);
    }
}
