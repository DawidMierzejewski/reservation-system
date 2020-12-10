using System;

namespace ReservationSystem.Catalog.Api.Contracts.Services
{
    public class GetOfferQuery
    {
        public long ServiceId { get; set; }

        public Guid UserId { get; set; }
    }
}
