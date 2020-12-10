using System.Collections.Generic;
using System.Linq;

namespace ReservationSystem.Scheduling.Api.Contracts.AvailableDates
{
    public class AvailableDates
    {
        public IEnumerable<AvailableDateItem> Items = Enumerable.Empty<AvailableDateItem>();
    }
}
