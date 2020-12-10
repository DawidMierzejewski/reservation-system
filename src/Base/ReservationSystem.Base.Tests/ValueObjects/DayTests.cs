using FluentAssertions;
using System;
using System.Collections.Generic;
using ReservationSystem.Base.Domain.ValueObjects.Day;
using Xunit;

namespace ReservationSystem.Base.Tests.ValueObjects
{
    public class DayTests
    {
        [Fact]
        public void Should_ConvertFromDateTime()
        {
            var now = DateTime.Now;

            var date = Date.FromDateTime(now);

            date.Year.Should().Be(now.Year);
            date.Month.Should().Be(now.Month);
            date.Day.Should().Be(now.Day);
        }

        [Fact]
        public void Should_BeComparable()
        {
            var now = DateTime.Now;

            var date1 = new Date(now);
            var date2 = new Date(now);

            var isEqual = date1
                .Equals(date2);

            isEqual.Should().BeTrue();

            var date3 = new Date(now.AddDays(2));

            isEqual = date1
                .Equals(date3);

            isEqual.Should().BeFalse();
        }

        [Fact]
        public void Should_BeUseAsDictionaryKey()
        {
            var date = new Date(DateTime.Now);

            Assert.Throws<ArgumentException>(() => new Dictionary<Date, bool>
            {
                {date, true},
                {date, true }
            });

            var date2 = new Date(DateTime.Now.AddDays(2));

            var dictionary = new Dictionary<Date, bool>
            {
                {date, true},
                {date2, false}
            };

            dictionary[date].Should().BeTrue();
            dictionary[date2].Should().BeFalse();
        }
    }
}
