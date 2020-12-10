namespace ReservationSystem.Catalog.Core.Application.Services.Queries
{
    public class GetServicesQuery
    {
        public int CategoryId { get; }

        public GetServicesQuery(int categoryId) => CategoryId = categoryId;
    }
}
