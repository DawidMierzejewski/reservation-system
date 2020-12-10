using MediatR;
using ReservationSystem.Base.Services.CQRS;
using System;
using System.Collections.Generic;

namespace ReservationSystem.Scheduling.Application.Commands.ConfigureDailySchedule
{
    public class ConfigureDailyScheduleCommand : CommandBase, IRequest<DailyScheduleId>
    {
        public long ServiceId { get; set; }

        public DateTime Day { get; set; }

        public IEnumerable<DateToSchedule> Dates { get; }

        public ConfigureDailyScheduleCommand(IEnumerable<DateToSchedule> dates, DateTime day, long serviceId)
        {
            Dates = dates;
            Day = day;
            ServiceId = serviceId;
        }
    }

    public class DateToSchedule
    {
        public DateTime FromDateTime { get; set; }

        public DateTime ToDateTime { get; set; }

        public DateToSchedule(DateTime fromDateTime, DateTime toDateTime)
        {
            FromDateTime = fromDateTime;
            ToDateTime = toDateTime;
        }
    }
}
