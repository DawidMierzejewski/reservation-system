using System.Collections.Generic;
using System.Linq;

namespace ReservationSystem.Catalog.Api.Contracts.Services
{
    public class Services
    {
        public IEnumerable<ServiceItem> Items = Enumerable.Empty<ServiceItem>();
    }
}
