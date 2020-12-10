using System;
using ReservationSystem.Base.Domain.ValueObjects.Day;
using ReservationSystem.Base.Domain.ValueObjects.TimeSlot;
using ReservationSystem.Scheduling.Domain.ScheduledDates.Exceptions;

namespace ReservationSystem.Scheduling.Domain.ScheduledDates
{
    public class ScheduledDate
    {
        public Guid DateId { get; private set; }

        public Guid ScheduleId { get; private set; }

        public TimeSlot TimeSlot { get; private set; }

        public Date Day => Date.FromDateTime(StartDateTime);

        public DateTime StartDateTime { get; private set; }

        public DateTime EndDateTime { get; private set; }

        public ScheduledDateStatus Status { get; private set; }

        public DateTime CreatedDate { get; private set; }

        public ScheduledDate(DateTime startDateTime, DateTime endDateTime)
        {
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            Status = ScheduledDateStatus.Available;
            TimeSlot = TimeSlot.FromDateTime(StartDateTime, EndDateTime);
        }

        public void Reserve()
        {
            if (!CanReserve())
            {
                throw new ScheduledDateCantBeReservedException(DateId);
            }

            Status = ScheduledDateStatus.Reserved;
        }

        public void Release()
        {
            if (!CanRelease())
            {
                throw new ScheduledDateCantBeReleasedException(DateId);
            }

            Status = ScheduledDateStatus.Available;
        }

        public bool IsAvailable()
        {
            return Status == ScheduledDateStatus.Available;
        }

        private bool CanReserve()
        {
            return Status == ScheduledDateStatus.Available;
        }

        private bool CanRelease()
        {
            return Status == ScheduledDateStatus.Reserved;
        }
    }
}
