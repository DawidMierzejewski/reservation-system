using System;

namespace ReservationSystem.WebApp.ApiGateway.Contracts.Availability
{
    public class GetAvailableDatesQuery
    {
        public long ServiceId { get; set; }

        public DateTime FromDateTime { get; set; }

        public DateTime ToDateTime { get; set; }
    }
}
