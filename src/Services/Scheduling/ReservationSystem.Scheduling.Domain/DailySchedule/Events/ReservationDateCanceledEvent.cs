using ReservationSystem.Base.Domain;
using ReservationSystem.Scheduling.Domain.ScheduledDates;

namespace ReservationSystem.Scheduling.Domain.DailySchedule.Events
{
    public class ReservationDateCanceledEvent : IDomainEvent
    {
        public ScheduledDate ScheduledDate { get; }

        public Domain.DailySchedule.DailySchedule DailySchedule { get; }

        public ReservationDateCanceledEvent(Domain.DailySchedule.DailySchedule dailySchedule, ScheduledDate scheduledDate)
        {
            DailySchedule = dailySchedule;
            ScheduledDate = scheduledDate;
        }
    }
}
