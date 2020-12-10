using System;

namespace ReservationSystem.Catalog.Core.Infrastructure.EntityFramework.Entities
{
    public class Service
    {
        public long ServiceId { get; }

        public int CategoryId { get; }

        public string ShortDescription { get; }

        public string LongDescription { get; }

        public string Title { get; }

        public decimal InitialPrice { get; }

        public string CurrencyCode { get; }

        public bool CanBeReserved { get; }

        public DateTime CreateDate { get; }

        public Guid CreatedBy { get; }

        public Service(int categoryId, string shortDescription, string longDescription, string title, decimal initialPrice, string currencyCode, bool canBeReserved, Guid createdBy)
            => (CategoryId, ShortDescription, LongDescription, Title, InitialPrice, CurrencyCode, CanBeReserved, CreatedBy) = (categoryId, shortDescription, longDescription, title, initialPrice, currencyCode, canBeReserved, createdBy);
    }
}
