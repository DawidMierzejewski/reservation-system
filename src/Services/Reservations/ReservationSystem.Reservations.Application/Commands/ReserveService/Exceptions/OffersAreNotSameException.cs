namespace ReservationSystem.Reservations.Application.Commands.ReserveService.Exceptions
{
    public class OffersAreNotSameException : Base.Services.Exceptions.ApplicationException
    {
        public override string Code => "offers_are_not_same";

        public decimal OfferPrice { get; }

        public string OfferCurrencyCode { get; }

        public decimal SeenOfferPrice { get; }

        public string SeenOfferCurrencyCode { get; }

        public OffersAreNotSameException(decimal offerPrice, 
            string offerCurrencyCode,
            decimal seenOfferPrice,
            string seenOfferCurrencyCode) : base("Offers are not same")
        {
            OfferPrice = offerPrice;
            OfferCurrencyCode = offerCurrencyCode;
            SeenOfferPrice = seenOfferPrice;
            SeenOfferCurrencyCode = seenOfferCurrencyCode;
        }
    }
}
