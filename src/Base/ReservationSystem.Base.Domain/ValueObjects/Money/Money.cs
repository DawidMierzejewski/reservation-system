using System;
using ReservationSystem.Base.Domain.ValueObjects.Money.Exceptions;

namespace ReservationSystem.Base.Domain.ValueObjects.Money
{
    public struct Money : IComparable, IEquatable<Money>
    {
        public decimal Amount { get; }

        public Currency Currency { get; }

        public Money(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public Money(double amount, Currency currency)
            : this((decimal)amount, currency)
        {

        }

        public bool IsZero()
        {
            return Amount == (decimal)0.00;
        }

        public static Money Zero(Currency currency)
        {
            return new Money(0.00, currency);
        }

        public static Money operator +(Money money1, Money money2)
        {
            AssertCurrencies(money1.Currency, money2.Currency);

            return new Money(money1.Amount + money2.Amount, money1.Currency);
        }

        public static Money operator -(Money money1, Money money2)
        {
            AssertCurrencies(money1.Currency, money2.Currency);

            return new Money(money1.Amount - money2.Amount, money1.Currency);
        }

        public static bool operator <(Money money1, Money money2)
        {
            return money1.CompareTo(money2) < 0;
        }

        public static bool operator >(Money money1, Money money2)
        {
            return money1.CompareTo(money2) > 0;
        }

        public static bool operator >=(Money money1, Money money2)
        {
            return money1.CompareTo(money2) >= 0;
        }

        public static bool operator <=(Money money1, Money money2)
        {
            return money1.CompareTo(money2) <= 0;
        }

        public static bool operator ==(Money money1, Money money2)
        {
            return money1.Equals(money2);
        }

        public static bool operator !=(Money money1, Money money2)
        {
            return !money1.Equals(money2);
        }

        public int CompareTo(object obj)
        {
            if (!(obj is Money))
            {
                throw new Exception();
            }

            var money = (Money)obj;

            AssertCurrencies(Currency, money.Currency);

            return Amount.CompareTo(money.Amount);
        }

        public override bool Equals(object obj)
        {
            return obj is Money money && Equals(money);
        }

        public bool Equals(Money other)
        {
            return Currency == other.Currency && Amount == other.Amount;
        }

        public override int GetHashCode()
        {
            return (Amount, Currency).GetHashCode();
        }

        public override string ToString()
        {
            return $"{Amount} {Currency.ToString()}";
        }

        private static void AssertCurrencies(Currency currency1, Currency currency2)
        {
            if (currency1 != currency2)
            {
                throw new CurrenciesAreDifferentException(currency1, currency2);
            }
        }
    }
}
