using ReservationSystem.Base.Domain;
using ReservationSystem.Scheduling.Domain.ScheduledDates;
using System.Collections.Generic;

namespace ReservationSystem.Scheduling.Domain.DailySchedule.Events
{
    public class DailyScheduleConfiguredEvent : IDomainEvent
    {
        public IEnumerable<ScheduledDate> ScheduledDates { get; }

        public DailySchedule DailySchedule { get; }

        public DailyScheduleConfiguredEvent(DailySchedule dailySchedule, IEnumerable<ScheduledDate> scheduledDates)
        {
            DailySchedule = dailySchedule;
            ScheduledDates = scheduledDates;
        }
    }
}
