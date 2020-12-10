using System;

namespace ReservationSystem.Reservations.Api.Contracts.Reservations
{
    public class ReserveServiceBody
    {
        public long ServiceId { get; set; }

        public Guid AvailableTermId { get; set; }

        public decimal Price { get; set; }

        public string CurrencyCode { get; set; }
    }
}
