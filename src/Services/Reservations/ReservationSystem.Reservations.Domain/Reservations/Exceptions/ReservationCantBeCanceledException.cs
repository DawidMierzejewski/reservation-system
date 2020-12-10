using ReservationSystem.Base.Domain.Exceptions;

namespace ReservationSystem.Reservations.Domain.Reservations.Exceptions
{
    public class ReservationCantBeCanceledException : DomainException
    {
        public override string Code => "reservation_cant_be_canceled";

        public long ReservationId { get; }

        public ReservationCantBeCanceledException(long reservationId)
        {
            ReservationId = reservationId;
        }
    }
}
