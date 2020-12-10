using System;

namespace ReservationSystem.Scheduling.Api.Contracts.AvailableDates
{
    public class GetAvailableDatesQueryString
    {
        public long ServiceId { get; set; }

        public DateTime FromDateTime { get; set; }

        public DateTime ToDateTime { get; set; }
    }
}
