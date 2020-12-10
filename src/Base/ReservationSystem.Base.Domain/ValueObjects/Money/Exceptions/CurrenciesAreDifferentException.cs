using System;

namespace ReservationSystem.Base.Domain.ValueObjects.Money.Exceptions
{
    public class CurrenciesAreDifferentException : InvalidOperationException
    {
        public CurrenciesAreDifferentException(Currency currency1, Currency currency2)
            : base($"Currencies are different. First currency: {currency1.Code}, Second currency {currency2.Code}")
        {
        }
    }
}
