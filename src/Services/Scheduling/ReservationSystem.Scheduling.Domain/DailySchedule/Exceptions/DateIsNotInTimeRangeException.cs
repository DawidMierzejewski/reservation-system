using ReservationSystem.Base.Domain.Exceptions;

namespace ReservationSystem.Scheduling.Domain.DailySchedule.Exceptions
{
    public class DateIsNotInTimeRangeException : DomainException
    {
        public override string Code => "date_is_not_in_time_range";
    }
}
