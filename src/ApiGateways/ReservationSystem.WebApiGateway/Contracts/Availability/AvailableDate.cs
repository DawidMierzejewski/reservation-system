using System;

namespace ReservationSystem.WebApp.ApiGateway.Contracts.Availability
{
    public class AvailableDate
    {
        public Guid DateId { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}