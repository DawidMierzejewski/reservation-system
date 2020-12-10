using ReservationSystem.Base.Domain;
using System;

namespace ReservationSystem.Scheduling.Domain.DailySchedule.Events
{
    public class DateRemovedEvent : IDomainEvent
    {
        public DailySchedule DailySchedule { get; }

        public Guid DateId { get; }

        public DateRemovedEvent(DailySchedule dailySchedule, Guid dateId)
        {
            DailySchedule = dailySchedule;
            DateId = dateId;
        }
    }
}
