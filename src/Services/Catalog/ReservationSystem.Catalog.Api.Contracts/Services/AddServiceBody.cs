namespace ReservationSystem.Catalog.Api.Contracts.Services
{
    public class AddServiceBody
    {
        public int CategoryId { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        public string Title { get; set; }

        public decimal InitialPrice { get; set; }

        public string CurrencyCode { get; set; }

        public bool CanBeReserved { get; set; }
    }
}
