using System.Collections.Generic;

namespace ReservationSystem.Reservations.Api.Contracts.Reservations
{
    public class Reservations
    {
        public IEnumerable<ReservationItem> ReservationItems = new List<ReservationItem>();
    }
}
