using System;
using System.Collections.Generic;

namespace ReservationSystem.Base.Domain.ValueObjects.Day
{
    public class Date : ValueObject
    {
        public int Year { get; }

        public int Month { get; }

        public int Day { get; }

        public Date(int year, int month, int day)
        {
            Year = year;
            Month = month;
            Day = day;
        }

        public Date(DateTime date)
        {
            Year = date.Year;
            Month = date.Month;
            Day = date.Day;
        }

        public static Date FromDateTime(DateTime date)
        {
            return new Date(date);
        }

        public DateTime ToDateTime()
        {
            return new DateTime(Year, Month, Day);
        }

        public bool Equals(Date date1, Date date2)
        {
            return date1.Year == date2.Year && date1.Month == date2.Month && date1.Day == date2.Day;
        }

        public int GetHashCode(Date obj)
        {
            return (Year, Month, Day).GetHashCode();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Year;
            yield return Month;
            yield return Day;
        }
    }
}
