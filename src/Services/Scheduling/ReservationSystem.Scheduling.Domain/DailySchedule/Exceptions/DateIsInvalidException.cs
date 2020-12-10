using ReservationSystem.Base.Domain.Exceptions;

namespace ReservationSystem.Scheduling.Domain.DailySchedule.Exceptions
{
    public class DateIsInvalidException : DomainException
    {
        public override string Code => "date_is_invalid";
    }
}
