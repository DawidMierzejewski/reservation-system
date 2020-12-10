using System;
using System.Collections.Generic;
using ReservationSystem.Base.Domain.ValueObjects.TimeSlot.Exceptions;

namespace ReservationSystem.Base.Domain.ValueObjects.TimeSlot
{
    public class TimeSlot : ValueObject
    {
        public TimeSpan StartTime { get; }
        public TimeSpan EndTime { get; }

        public TimeSlot(TimeSpan startTime, TimeSpan endTime)
        {
            if (startTime > endTime)
            {
                throw new StartDateCannotBeGreaterThanEndDateException();
            }

            StartTime = startTime;
            EndTime = endTime;
        }

        public bool IsInTimeRange(TimeSlot timeSlot)
        {
            return timeSlot.StartTime >= StartTime && timeSlot.StartTime <= EndTime ||
                   timeSlot.EndTime >= StartTime && timeSlot.EndTime <= EndTime;
        }

        public static TimeSlot FromDateTime(DateTime startDateTime, DateTime endDateTime)
        {
            return new TimeSlot(startDateTime.TimeOfDay, endDateTime.TimeOfDay);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return StartTime;
            yield return EndTime;
        }
    }
}
