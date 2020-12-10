using MediatR;
using ReservationSystem.Base.Services.CQRS;
using ReservationSystem.Base.Services.MediatR;

namespace ReservationSystem.Reservations.Application.Commands.CancelReservation
{
    public class CancelReservationCommand : CommandBase, IRequest<EmptyResult>
    {
        public long ReservationId { get; set; }

        public CancelReservationCommand(long reservationId) => ReservationId = reservationId;
    }
}
