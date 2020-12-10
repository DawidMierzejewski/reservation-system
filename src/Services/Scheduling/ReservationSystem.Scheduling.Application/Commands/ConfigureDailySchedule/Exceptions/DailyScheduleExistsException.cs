using System;

namespace ReservationSystem.Scheduling.Application.Commands.ConfigureDailySchedule.Exceptions
{
    public class DailyScheduleExistsException : Base.Services.Exceptions.ApplicationException
    {
        public override string Code => "daily_schedule_exists";

        public Guid Id { get; }

        public DailyScheduleExistsException(Guid id)
        {
            Id = id;
        }
    }
}
