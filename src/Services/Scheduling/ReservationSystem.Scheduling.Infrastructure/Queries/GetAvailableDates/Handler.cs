using MediatR;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Scheduling.Api.Contracts.AvailableDates;
using ReservationSystem.Scheduling.Infrastructure.EntityFramework;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ReservationSystem.Scheduling.Domain.ScheduledDates;

namespace ReservationSystem.Scheduling.Infrastructure.Queries.GetAvailableDates
{
    public class Handler : IRequestHandler<GetAvailableDatesQuery, AvailableDates>
    {
        private readonly SchedulingContext _schedulingContext;

        public Handler(SchedulingContext schedulingContext)
        {
            _schedulingContext = schedulingContext;
        }

        public async Task<AvailableDates> Handle(GetAvailableDatesQuery request, CancellationToken cancellationToken)
        {
            var dates = await _schedulingContext.ScheduledDates
                .Where(s => s.StartDateTime >= request.FromDate 
                && s.EndDateTime <= request.ToDate 
                && s.Status == ScheduledDateStatus.Available).ToArrayAsync(cancellationToken);

            return new AvailableDates
            {
                Items = dates.Select(d => new AvailableDateItem
                {
                    DateId = d.DateId,
                    Day = d.Day.Day,
                    Month = d.Day.Month,
                    Year = d.Day.Year,
                    EndTime = d.TimeSlot.EndTime,
                    StartTime = d.TimeSlot.StartTime,
                    StartDateTime = d.StartDateTime,
                    EndDateTime = d.EndDateTime
                })
            };
        }
    }
}
