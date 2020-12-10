using ReservationSystem.Base.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace ReservationSystem.Reservations.Domain.Offer
{
    public class Offer : ValueObject
    {
        public decimal Price { get; }

        public string CurrencyCode { get; }

        public bool IsFree => Price.Equals(0);

        public Offer(decimal price, string currencyCode)
        {
            Price = price;
            CurrencyCode = currencyCode;
        }

        public bool AreSame(Offer offer)
        {
            return Equals(offer);
        }

        public bool Equals(Offer other)
        {
            return Price.Equals(other.Price) &&
                   CurrencyCode.Equals(other.CurrencyCode, StringComparison.InvariantCultureIgnoreCase);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Price;
            yield return CurrencyCode;
        }
    }
}
