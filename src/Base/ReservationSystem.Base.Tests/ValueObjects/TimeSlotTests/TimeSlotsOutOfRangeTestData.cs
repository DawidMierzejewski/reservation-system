using System;
using ReservationSystem.Base.Domain.ValueObjects.TimeSlot;
using Xunit;

namespace ReservationSystem.Base.Tests.ValueObjects.TimeSlotTests
{
    public class TimeSlotsOutOfRangeTestData : TheoryData<TimeSlot>
    {
        public TimeSlotsOutOfRangeTestData()
        {
            Add(new TimeSlot(
                new TimeSpan(10, 13, 0),
                new TimeSpan(10, 14, 0)));
            Add(new TimeSlot(
                new TimeSpan(10, 31, 0),
                new TimeSpan(10, 32, 0)));
            Add(new TimeSlot(
                new TimeSpan(11, 15, 0),
                new TimeSpan(11, 30, 0)));
            Add(new TimeSlot(
                new TimeSpan(9, 15, 0),
                new TimeSpan(9, 30, 0)));
            Add(new TimeSlot(
                new TimeSpan(10, 31, 0),
                new TimeSpan(10, 40, 0)));
        }
    }
}
