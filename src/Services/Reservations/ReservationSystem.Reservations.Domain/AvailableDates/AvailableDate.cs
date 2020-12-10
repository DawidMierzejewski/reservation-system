using System;
using ReservationSystem.Base.Domain.ValueObjects.Day;
using ReservationSystem.Base.Domain.ValueObjects.TimeSlot;

namespace ReservationSystem.Reservations.Domain.AvailableDates
{
    public class AvailableDate
    {
        public Guid DateId { get; private set; }

        public TimeSlot TimeSlot => TimeSlot.FromDateTime(StartDateTime, EndDateTime);

        public Date Day => Date.FromDateTime(StartDateTime);

        public DateTime StartDateTime { get; private set; }

        public DateTime EndDateTime { get; private set; }

        public bool IsAvailable { get; private set; }

        public AvailableDate(Guid dateId, DateTime startDateTime, DateTime endDateTime, bool isAvailable)
        {
            DateId = dateId;
            IsAvailable = isAvailable;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
        }
    }
}
