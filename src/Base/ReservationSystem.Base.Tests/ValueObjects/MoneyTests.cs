using FluentAssertions;
using ReservationSystem.Base.Domain.ValueObjects.Money;
using ReservationSystem.Base.Domain.ValueObjects.Money.Exceptions;
using Xunit;
using Currency = ReservationSystem.Base.Domain.ValueObjects.Money.Currency;

namespace ReservationSystem.Base.Tests.ValueObjects
{
    public class MoneyTests
    {
        [Theory]
        [InlineData(1.0, 2.0, 3.0)]
        [InlineData(1.15, 2.20, 3.35)]
        [InlineData(1.99, 2.99, 4.98)]
        [InlineData(0, 0, 0)]
        public void Should_Add(decimal v1, decimal v2, decimal expectedValue)
        {
            var pln = Currency.PLN;

            var money1 = new Money(v1, pln);
            var money2 = new Money(v2, pln);

            var added = money1 + money2;

            added
                .Amount
                .Should()
                .Be(expectedValue);
        }

        [Theory]
        [InlineData(2.0, 1.0, 1.0)]
        [InlineData(1.0, 2.0, -1.0)]
        [InlineData(2.99, 1.99, 1.0)]
        [InlineData(0, 0, 0)]
        public void Should_Subtract(decimal v1, decimal v2, decimal expectedValue)
        {
            var pln = Currency.PLN;

            var money1 = new Money(v1, pln);
            var money2 = new Money(v2, pln);

            var added = money1 - money2;

            added
                .Amount
                .Should()
                .Be(expectedValue);
        }

        [Fact]
        public void Should_FailToAdd_When_CurrenciesAreDifferent()
        {
            var usd = new Money(2.0, Currency.USD);

            var pln = new Money(1.0, Currency.PLN);

            Assert.Throws<CurrenciesAreDifferentException>(() => pln + usd);
        }

        [Fact]
        public void Should_FailToSubstract_When_CurrenciesAreDifferent()
        {
            var usd = new Money(2.0, Currency.USD);

            var pln = new Money(1.0, Currency.PLN);

            Assert.Throws<CurrenciesAreDifferentException>(() => usd - pln);
        }

        [Fact]
        public void Should_FailToCompare_When_CurrenciesAreDifferent()
        {
            var usd = new Money(2.0, Currency.USD);

            var pln = new Money(1.0, Currency.PLN);

            Assert.Throws<CurrenciesAreDifferentException>(() => usd > pln);
            Assert.Throws<CurrenciesAreDifferentException>(() => usd >= pln);
            Assert.Throws<CurrenciesAreDifferentException>(() => usd < pln);
            Assert.Throws<CurrenciesAreDifferentException>(() => usd <= pln);
        }

        [Fact]
        public void Should_Compare()
        {
            var usd = new Money(2.0, Currency.USD);

            var pln = new Money(2.0, Currency.PLN);

            var isEqual = usd == pln;
            isEqual.Should().BeFalse();

            isEqual = usd != pln;
            isEqual.Should().BeTrue();
        }
    }
}
