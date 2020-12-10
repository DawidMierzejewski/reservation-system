namespace ReservationSystem.Reservations.Domain.Reservations
{
    public struct ReservationId
    {
        public long Value { get; set; }

        public ReservationId(long value) => Value = value;
    }
}
