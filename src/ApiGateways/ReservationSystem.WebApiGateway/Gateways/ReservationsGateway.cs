using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ReservationSystem.Base.Extensions;
using ReservationSystem.Reservations.Api.Contracts;
using ReservationSystem.WebApp.ApiGateway.Contracts.Reservations;

namespace ReservationSystem.WebApp.ApiGateway.Gateways
{
    public class ReservationsGateway : IReservationsGateway
    {
        private readonly IReservationsApiClient _reservationsApiClient;

        public ReservationsGateway(IReservationsApiClient reservationsApiClient)
        {
            _reservationsApiClient = reservationsApiClient;
        }

        public async Task<ReservationDetails> GetReservationDetails(long reservationId, CancellationToken cancellationToken = default)
        {
            var reservationDetails = await _reservationsApiClient.GetReservationDetails(reservationId, cancellationToken);
            if (reservationDetails == null)
            {
                return null;;
            }

            return new ReservationDetails
            {
                ReservationId = reservationDetails.ReservationId
            };
        }

        public async Task<Contracts.Reservations.Reservations> GetReservations(CancellationToken cancellationToken = default)
        {
            var reservations = await _reservationsApiClient.GetReservations(cancellationToken);
            if (reservations == null || reservations.ReservationItems.IsNullOrEmpty())
            {
                return new Contracts.Reservations.Reservations();
            }

            var reservationItems = reservations.ReservationItems.Select(item => new ReservationItem
            {
                ReservationId = item.ReservationId
            });

            return new Contracts.Reservations.Reservations
            {
                Items = reservationItems
            };
        }

        public async Task<ReservedServiceId> ReserveService(ReserveServiceBody reserveServiceBody, CancellationToken cancellationToken = default)
        {
            var reservedService = await _reservationsApiClient.ReserveService(
                new Reservations.Api.Contracts.Reservations.ReserveServiceBody
                {
                    ServiceId = reserveServiceBody.ServiceId,
                    AvailableTermId = reserveServiceBody.AvailableTermId,
                    CurrencyCode = reserveServiceBody.CurrencyCode,
                    Price = reserveServiceBody.Price
                }, 
                cancellationToken);

            return new ReservedServiceId
            {
                Value = reservedService.ReservationId
            };
        }

        public async Task CancelReservation(long reservationId, CancellationToken cancellationToken = default)
        {
            await _reservationsApiClient.CancelReservation(reservationId, cancellationToken);
        }
    }
}