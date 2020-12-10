using System.Collections.Generic;
using System.Linq;

namespace ReservationSystem.WebApp.ApiGateway.Contracts.Reservations
{
    public class Reservations
    {
        public IEnumerable<ReservationItem> Items = Enumerable.Empty<ReservationItem>();
    }
}
