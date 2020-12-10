namespace ReservationSystem.Reservations.Application.Commands.CancelReservation.Exceptions
{
    public class ReservationDoesNotExist : Base.Services.Exceptions.ApplicationException
    {
        public override string Code => "reservation_does_not_exists";

        public long ReservationId { get; }

        public ReservationDoesNotExist(long reservationId) : base($"Reservation does not exist serviceId {reservationId}")
        {
            ReservationId = reservationId;
        }
    }
}
