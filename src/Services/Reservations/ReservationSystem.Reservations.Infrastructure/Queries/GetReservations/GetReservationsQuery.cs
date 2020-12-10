using MediatR;

namespace ReservationSystem.Reservations.Infrastructure.Queries.GetReservations
{
    public class GetReservationsQuery : IRequest<Api.Contracts.Reservations.Reservations>
    {
        public GetReservationsQuery()
        {

        }
    }
}
