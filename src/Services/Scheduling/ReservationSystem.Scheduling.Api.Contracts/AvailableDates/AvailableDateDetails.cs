﻿using System;

namespace ReservationSystem.Scheduling.Api.Contracts.AvailableDates
{
    public class AvailableDateDetails
    {
        public Guid DateId { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public int Day { get; set;  }

        public bool IsAvailable { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }
    }
}
