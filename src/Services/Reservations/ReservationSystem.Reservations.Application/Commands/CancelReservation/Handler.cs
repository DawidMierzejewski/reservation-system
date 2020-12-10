using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ReservationSystem.Base.Services.Events;
using ReservationSystem.Base.Services.Identity;
using ReservationSystem.Base.Services.MediatR;
using ReservationSystem.Reservations.Domain.Reservations;
using ReservationSystem.Base.Services.Exceptions;
using ReservationSystem.Base.Time;
using ReservationSystem.Reservations.Application.Commands.CancelReservation.Exceptions;

namespace ReservationSystem.Reservations.Application.Commands.CancelReservation
{
    public class Handler : IRequestHandler<CancelReservationCommand, EmptyResult>
    {
        private readonly IReservationsRepository _reservationRepository;
        private readonly IMessagePublisher _messagePublisher;
        private readonly IIdentityContext _identityContext;
        private readonly IClock _clock;

        public Handler(IReservationsRepository reservationRepository, 
            IMessagePublisher messagePublisher, 
            IIdentityContext identityContext,
            IClock clock)
        {
            _reservationRepository = reservationRepository;
            _messagePublisher = messagePublisher;
            _identityContext = identityContext;
            _clock = clock;
        }

        public async Task<EmptyResult> Handle(CancelReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _reservationRepository.GetAsync(request.ReservationId, cancellationToken);
            if (reservation == null)
            {
                throw new ReservationDoesNotExist(request.ReservationId);
            }

            if (reservation.UserId != _identityContext.UserId)
            {
                throw new UnauthorizedOperationException(nameof(CancelReservationCommand), _identityContext.UserId, reservation.UserId.ToString());
            }

            reservation.Cancel(_clock.UtcNow);

            await _reservationRepository.UpdateAsync(reservation, cancellationToken);

            await _messagePublisher.PublishAsync(reservation.GetChanges());

            return EmptyResultFactory.Create();
        }
    }
}
