using System;
using System.Collections.Generic;
using System.Linq;
using ReservationSystem.Base.Domain;
using ReservationSystem.Base.Domain.ValueObjects.Day;
using ReservationSystem.Scheduling.Domain.DailySchedule.Events;
using ReservationSystem.Scheduling.Domain.DailySchedule.Exceptions;
using ReservationSystem.Scheduling.Domain.ScheduledDates;

namespace ReservationSystem.Scheduling.Domain.DailySchedule
{
    public class DailySchedule : Aggregate
    {
        public Guid ScheduleId { get; private set; }

        public long ServiceId { get; private set; }

        public Date Day { get; private set; }

        public IEnumerable<ScheduledDate> Dates => DatesList.AsReadOnly();

        private List<ScheduledDate> DatesList { get; set; }

        public DateTime CreatedDate { get; private set; }

        protected DailySchedule()
        {
            DatesList = new List<ScheduledDate>();
        }

        public DailySchedule(Date day, long serviceId)
        {
            Day = day;
            ServiceId = serviceId;
            DatesList = new List<ScheduledDate>();
        }

        public void RemoveDate(Guid scheduledId)
        {
            var scheduledDate = DatesList.FirstOrDefault(d => d.DateId == scheduledId);
            if (scheduledDate == null)
            {
                throw new DateDoesNotExistException(scheduledId);
            }

            DatesList.Remove(scheduledDate);

            Changes.Add(new DateCreatedEvent(this, scheduledDate));
        }

        public void ReleaseDate(Guid scheduledId)
        {
            var scheduledDate = DatesList.FirstOrDefault(d => d.DateId == scheduledId);
            if (scheduledDate == null)
            {
                throw new DateDoesNotExistException(scheduledId);
            }

            scheduledDate.Release();

            Changes.Add(new DateReleasedEvent(this, scheduledDate));
        }

        public void ReserveDate(Guid scheduledId)
        {
            var scheduledDate = DatesList.FirstOrDefault(d => d.DateId == scheduledId);
            if (scheduledDate == null)
            {
                throw new DateDoesNotExistException(scheduledId);
            }

            scheduledDate.Reserve();

            Changes.Add(new DateCreatedEvent(this, scheduledDate));
        }

        public void ScheduleDate(ScheduledDate date)
        {
            if (!date.Day.Equals(Day))
            {
                throw new DateIsInvalidException();
            }

            if (DatesList.Any(t => t.TimeSlot.IsInTimeRange(date.TimeSlot)))
            {
                throw new DateIsNotInTimeRangeException();
            }

            DatesList.Add(date);

            Changes.Add(new DateCreatedEvent(this, date));
        }

        public void ScheduleDates(IEnumerable<ScheduledDate> dates)
        {
            CheckIfCanConfigureDates(dates);

            foreach (var date in dates)
            {
                DatesList.Add(date);
            }

            Changes.Add(new DailyScheduleConfiguredEvent(this, dates));
        }

        private void CheckIfCanConfigureDates(IEnumerable<ScheduledDate> dates)
        {
            if (dates.Any(d => !d.Day.Equals(Day)))
            {
                throw new DateIsInvalidException();
            }

            foreach (var date in dates)
            {
                if (DatesList.Any(t => t.TimeSlot.IsInTimeRange(date.TimeSlot)))
                {
                    throw new DateIsNotInTimeRangeException();
                }
            }
            
            var tempDates = dates.ToList();

            for (var i = tempDates.Count - 1; i >= 0; i--)
            {
                var term = tempDates[i];
                tempDates.RemoveAt(i);

                if (tempDates.Any(t => t.TimeSlot.IsInTimeRange(term.TimeSlot)))
                {
                    throw new DateIsNotInTimeRangeException();
                }
            }
        }
    }
}
