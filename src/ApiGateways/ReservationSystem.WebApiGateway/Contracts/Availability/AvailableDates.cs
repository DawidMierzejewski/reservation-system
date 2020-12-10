using System.Collections.Generic;
using System.Linq;

namespace ReservationSystem.WebApp.ApiGateway.Contracts.Availability
{
    public class AvailableDates
    {
        public IEnumerable<AvailableDate> Items = Enumerable.Empty<AvailableDate>();
    }
}
