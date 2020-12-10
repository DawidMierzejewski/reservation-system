namespace ReservationSystem.Catalog.Core.Application.Services.Queries
{
    public class GetServiceDetailsQuery
    {
        public long ServiceId { get;}

        public GetServiceDetailsQuery(long serviceId) => ServiceId = serviceId;
    }
}
