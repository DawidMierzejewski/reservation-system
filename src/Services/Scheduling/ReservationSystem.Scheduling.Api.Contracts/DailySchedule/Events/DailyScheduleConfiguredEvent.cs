using System;
using ReservationSystem.Base;

namespace ReservationSystem.Scheduling.Api.Contracts.DailySchedule.Events
{
    public class DailyScheduleConfiguredEvent : IntegrationEvent
    {
        public string ObjectId { get; }

        public Guid ScheduleId { get; }

        public DailyScheduleConfiguredEvent(Guid scheduleId)
        {
            ScheduleId = scheduleId;
            ObjectId = ScheduleId.ToString();
        }
    }
}
