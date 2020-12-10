using ReservationSystem.Base.Domain;

namespace ReservationSystem.Reservations.Domain.Reservations.Events
{
    public class ReservationCancelledEvent : IDomainEvent
    {
        public Reservation Reservation { get; set; }

        public ReservationCancelledEvent(Reservation reservation)
        {
            Reservation = reservation;
        }
    }
}
