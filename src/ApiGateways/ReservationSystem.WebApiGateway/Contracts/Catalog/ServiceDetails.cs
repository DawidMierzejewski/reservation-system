namespace ReservationSystem.WebApp.ApiGateway.Contracts.Catalog
{
    public class ServiceDetails
    {
        public long ServiceId { get; set; }

        public int CategoryId { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        public string Title { get; set; }

        public decimal InitialPrice { get; set; }

        public string CurrencyCode { get; set; }

        public bool CanBeReserved { get; set; }
    }
}
