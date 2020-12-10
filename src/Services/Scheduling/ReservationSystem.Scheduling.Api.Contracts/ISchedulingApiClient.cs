using System;
using Refit;
using System.Threading;
using System.Threading.Tasks;
using ReservationSystem.Scheduling.Api.Contracts.AvailableDates;

namespace ReservationSystem.Scheduling.Api.Contracts
{
    public interface ISchedulingApiClient
    {
        [Get("/api/available-dates")]
        Task<AvailableDates.AvailableDates> GetAvailableDates([Query] GetAvailableDatesQueryString query, CancellationToken cancellationToken);

        [Get("/api/available-dates/{dateId}")]
        Task<AvailableDateDetails> GetAvailableDateDetails([AliasAs("dateId")] Guid dateId, CancellationToken cancellationToken);
    }
}
