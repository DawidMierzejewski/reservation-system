using System.Threading;
using System.Threading.Tasks;
using ReservationDetails = ReservationSystem.WebApp.ApiGateway.Contracts.Reservations.ReservationDetails;
using ReservedServiceId = ReservationSystem.WebApp.ApiGateway.Contracts.Reservations.ReservedServiceId;
using ReserveServiceBody = ReservationSystem.WebApp.ApiGateway.Contracts.Reservations.ReserveServiceBody;

namespace ReservationSystem.WebApp.ApiGateway.Gateways
{
    public interface IReservationsGateway
    {
        Task<ReservationDetails> GetReservationDetails(long reservationId, CancellationToken cancellationToken = default);

        Task<Contracts.Reservations.Reservations> GetReservations(CancellationToken cancellationToken = default);

        Task<ReservedServiceId> ReserveService(ReserveServiceBody reserveServiceBody, CancellationToken cancellationToken = default);

        Task CancelReservation(long reservationId, CancellationToken cancellationToken = default);
    }
}
