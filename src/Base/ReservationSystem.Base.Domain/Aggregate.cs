using System.Collections.Generic;
using System.Linq;

namespace ReservationSystem.Base.Domain
{
    public class Aggregate
    {
        protected List<IDomainEvent> Changes = new List<IDomainEvent>();

        public byte[] Timestamp { get; set; }

        protected void ApplyChanges(IDomainEvent @event)
        {
            Changes.Add(@event);
        }

        public IEnumerable<IDomainEvent> GetChanges()
        {
            return Changes.AsEnumerable();
        }
    }
}
