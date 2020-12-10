using System;
using ReservationSystem.Base.Domain.ValueObjects.Day;
using ReservationSystem.Base.Domain.ValueObjects.TimeSlot;
using ReservationSystem.Reservations.Domain.AvailableDates;

namespace ReservationSystem.Reservations.Tests.TestDataBuilders
{
    public class AvailableDateBuilder
    {
        private Date _day = Date.FromDateTime(DateTime.Now);
        private TimeSlot _timeSlot = new TimeSlot(new TimeSpan(10, 0, 0), new TimeSpan(11, 0, 0));

        private bool _isFree;

        public AvailableDateBuilder IsFree(bool isFree)
        {
            _isFree = isFree;

            return this;
        }

        public AvailableDateBuilder WithTimeSlot(TimeSlot timeSlot)
        {
            this._timeSlot = timeSlot;

            return this;
        }

        public AvailableDateBuilder WithDay(Date day)
        {
            this._day = day;
            return this;
        }

        public AvailableDate Build()
        {
            return new AvailableDate(Guid.NewGuid(), 
                _day.ToDateTime().Add(_timeSlot.StartTime),
                _day.ToDateTime().Add(_timeSlot.EndTime),
                _isFree);
        }
    }
}
