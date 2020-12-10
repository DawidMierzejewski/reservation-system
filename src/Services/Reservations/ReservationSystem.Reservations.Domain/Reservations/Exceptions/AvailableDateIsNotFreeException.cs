using ReservationSystem.Base.Domain.Exceptions;

namespace ReservationSystem.Reservations.Domain.Reservations.Exceptions
{
    public class AvailableDateIsNotFreeException : DomainException
    {
        public override string Code => "available_date_is_not_free";

        public AvailableDateIsNotFreeException()
        {

        }
    }
}
