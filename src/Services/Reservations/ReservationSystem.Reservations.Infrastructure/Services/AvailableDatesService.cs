using System;
using System.Threading;
using System.Threading.Tasks;
using ReservationSystem.Base.Domain.Attributes;
using ReservationSystem.Reservations.Domain.AvailableDates;
using ReservationSystem.Scheduling.Api.Contracts;

namespace ReservationSystem.Reservations.Infrastructure.Services
{
    [Service]
    public class AvailableDatesService : IAvailableDatesService
    {
        private readonly ISchedulingApiClient _schedulingApiClient;

        public AvailableDatesService(ISchedulingApiClient schedulingApiClient)
        {
            _schedulingApiClient = schedulingApiClient;
        }

        public async Task<AvailableDate> GetAvailableDate(Guid dateId, CancellationToken cancellationToken)
        {
            var dateDetails = await _schedulingApiClient.GetAvailableDateDetails(dateId, cancellationToken);

            return new AvailableDate(dateDetails.DateId,
                dateDetails.StartDateTime,
                dateDetails.EndDateTime,
                dateDetails.IsAvailable);
        }
    }
}
