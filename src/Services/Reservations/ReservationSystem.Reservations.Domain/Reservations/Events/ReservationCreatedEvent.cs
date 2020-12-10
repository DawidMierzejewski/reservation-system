using ReservationSystem.Base.Domain;

namespace ReservationSystem.Reservations.Domain.Reservations.Events
{
    public class ReservationCreatedEvent : IDomainEvent
    {
        public Reservation Reservation { get; set; }

        public ReservationCreatedEvent(Reservation reservation)
        {
            Reservation = reservation;
        }
    }
}
