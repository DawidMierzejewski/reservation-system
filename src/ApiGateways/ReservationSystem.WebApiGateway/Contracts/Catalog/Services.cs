using System.Collections.Generic;
using System.Linq;

namespace ReservationSystem.WebApp.ApiGateway.Contracts.Catalog
{
    public class Services
    {
        public IEnumerable<ServiceItem> Items = Enumerable.Empty<ServiceItem>();
    }
}
