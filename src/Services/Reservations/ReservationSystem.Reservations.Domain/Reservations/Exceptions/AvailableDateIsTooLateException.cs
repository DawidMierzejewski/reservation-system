using ReservationSystem.Base.Domain.Exceptions;

namespace ReservationSystem.Reservations.Domain.Reservations.Exceptions
{
    public class AvailableDateIsTooLateException : DomainException
    {
        public override string Code => "available_date_is_too_late";

        public AvailableDateIsTooLateException()
        {

        }
    }
}
