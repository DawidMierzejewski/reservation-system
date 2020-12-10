using System.Threading;
using System.Threading.Tasks;
using Refit;
using ReservationSystem.Reservations.Api.Contracts.Reservations;

namespace ReservationSystem.Reservations.Api.Contracts
{
    public interface IReservationsApiClient
    {
        [Get("/api/reservations")]
        Task<Reservations.Reservations> GetReservations(CancellationToken cancellationToken);

        [Get("/api/reservations/{reservationId}")]
        Task<ReservationDetails> GetReservationDetails([AliasAs("reservationId")] long reservationId, CancellationToken cancellationToken);

        [Post("/api/reservations")]
        Task<ReservedService> ReserveService([Body] ReserveServiceBody reserveServiceBody, CancellationToken cancellationToken);

        [Delete("/api/reservations/{reservationId}")]
        Task CancelReservation([AliasAs("reservationId")] long reservationId, CancellationToken cancellationToken);
    }
}
