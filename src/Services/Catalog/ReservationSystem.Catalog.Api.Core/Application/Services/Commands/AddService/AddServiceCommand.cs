namespace ReservationSystem.Catalog.Core.Application.Services.Commands.AddService
{
    public class AddServiceCommand : CommandBase
    {
        public int CategoryId { get; }

        public string ShortDescription { get; }

        public string LongDescription { get; }

        public string Title { get; }

        public decimal InitialPrice { get; }

        public string CurrencyCode { get; }

        public bool CanBeReserved { get; }

        public AddServiceCommand(int categoryId, string shortDescription, string longDescription, string title, decimal initialPrice, string currencyCode, bool canBeReserved)
            => (CategoryId, ShortDescription, LongDescription, Title, InitialPrice, CurrencyCode, CanBeReserved) = (categoryId, shortDescription, longDescription, title, initialPrice, currencyCode, canBeReserved);
    }
}
