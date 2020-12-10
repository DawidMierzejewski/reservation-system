using MediatR;
using ReservationSystem.Reservations.Api.Contracts.Reservations;

namespace ReservationSystem.Reservations.Infrastructure.Queries.GetReservationDetails
{
    public class GetReservationDetailsQuery : IRequest<ReservationDetails>
    {
        public long ReservationId { get; set; }

        public GetReservationDetailsQuery(long reservationId) => (ReservationId) = reservationId;
    }
}
