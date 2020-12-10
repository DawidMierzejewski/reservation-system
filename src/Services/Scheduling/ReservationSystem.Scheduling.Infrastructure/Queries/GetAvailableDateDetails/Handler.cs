using MediatR;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Scheduling.Api.Contracts.AvailableDates;
using ReservationSystem.Scheduling.Infrastructure.EntityFramework;
using System.Threading;
using System.Threading.Tasks;

namespace ReservationSystem.Scheduling.Infrastructure.Queries.GetAvailableDateDetails
{
    public class Handler : IRequestHandler<GetAvailableDateDetailsQuery, AvailableDateDetails>
    {
        private readonly SchedulingContext _schedulingContext;

        public Handler(SchedulingContext schedulingContext)
        {
            _schedulingContext = schedulingContext;
        }

        public async Task<AvailableDateDetails> Handle(GetAvailableDateDetailsQuery request, CancellationToken cancellationToken)
        {
            var date = await _schedulingContext.ScheduledDates
                .SingleOrDefaultAsync(s => s.DateId == request.DateId, cancellationToken);

            return new AvailableDateDetails
            {
                DateId = date.DateId,
                Day = date.Day.Day,
                Month = date.Day.Month,
                Year = date.Day.Year,
                IsAvailable = date.IsAvailable(),
                StartDateTime = date.StartDateTime,
                EndDateTime = date.EndDateTime
            };
        }
    }
}
