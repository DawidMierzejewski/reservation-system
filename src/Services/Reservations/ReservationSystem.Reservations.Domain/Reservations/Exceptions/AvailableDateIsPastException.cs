using ReservationSystem.Base.Domain.Exceptions;

namespace ReservationSystem.Reservations.Domain.Reservations.Exceptions
{
    public class AvailableDateIsPastException : DomainException
    {
        public override string Code => "available_date_is_past";

        public AvailableDateIsPastException()
        {

        }
    }
}
