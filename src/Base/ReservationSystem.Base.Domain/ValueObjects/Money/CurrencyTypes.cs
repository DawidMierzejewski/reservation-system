namespace ReservationSystem.Base.Domain.ValueObjects.Money
{
    public partial struct Currency
    {
        public static Currency PLN => new Currency("PLN");
        public static Currency USD => new Currency("USD");
        public static Currency GBP => new Currency("GBP");

        public static Currency EUR => new Currency("EUR");

    }
}
