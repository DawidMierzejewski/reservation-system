using System;

namespace ReservationSystem.Base.Domain.ValueObjects.Money
{
    public partial struct Currency : IEquatable<Currency>
    {
        public string Code { get; private set; }

        public Currency(string code) : this()
        {
            SetCode(code);
        }

        private void SetCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new Exception(code);
            }

            Code = code;
        }

        public static bool operator ==(Currency currency1, Currency currency2)
        {
            return currency1.Equals(currency2);
        }

        public static bool operator !=(Currency currency1, Currency currency2)
        {
            return !currency1.Equals(currency2);
        }

        public override bool Equals(object obj)
        {
            return obj is Currency currency && Equals(currency);
        }

        public bool Equals(Currency other)
        {
            return Code.Equals(other.Code, StringComparison.InvariantCultureIgnoreCase);
        }

        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }

        public override string ToString()
        {
            return Code;
        }
    }
}
