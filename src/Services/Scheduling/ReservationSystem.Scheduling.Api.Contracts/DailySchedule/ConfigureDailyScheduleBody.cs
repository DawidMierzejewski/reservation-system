using System;
using System.Collections.Generic;
using System.Linq;

namespace ReservationSystem.Scheduling.Api.Contracts.DailySchedule
{
    public class ConfigureDailyScheduleBody
    {
        public long ServiceId { get; set; }

        public DateTime Day { get; set; }

        public IEnumerable<DateToSchedule> Dates { get; } = Enumerable.Empty<DateToSchedule>();
    }

    public class DateToSchedule
    {
        public DateTime FromDateTime { get; set; }

        public DateTime ToDateTime { get; set; }
    }
}
