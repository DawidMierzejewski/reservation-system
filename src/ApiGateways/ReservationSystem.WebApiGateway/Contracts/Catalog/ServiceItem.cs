namespace ReservationSystem.WebApp.ApiGateway.Contracts.Catalog
{
    public class ServiceItem
    {
        public long ServiceId { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        public decimal InitialPrice { get; set; }
    }
}