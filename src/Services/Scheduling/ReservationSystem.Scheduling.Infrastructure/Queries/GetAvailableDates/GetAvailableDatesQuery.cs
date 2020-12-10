using MediatR;
using ReservationSystem.Scheduling.Api.Contracts.AvailableDates;
using System;

namespace ReservationSystem.Scheduling.Infrastructure.Queries.GetAvailableDates
{
    public class GetAvailableDatesQuery : IRequest<AvailableDates>
    {
        public long ServiceId { get; }

        public DateTime FromDate { get; }

        public DateTime ToDate { get; }

        public GetAvailableDatesQuery(long serviceId, DateTime fromDate, DateTime toDate)
        {
            ServiceId = serviceId;
            FromDate = fromDate;
            ToDate = toDate;
        }
    }
}
