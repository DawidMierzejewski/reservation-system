using System;

namespace ReservationSystem.Scheduling.Application.Commands.ConfigureDailySchedule
{
    public class DailyScheduleId
    {
        public Guid Value { get; set; }

        public DailyScheduleId(Guid value)
        {
            Value = value;
        }
    }
}
