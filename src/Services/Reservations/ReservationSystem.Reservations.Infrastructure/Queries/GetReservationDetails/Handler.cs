using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ReservationSystem.Base.Services.Exceptions;
using ReservationSystem.Base.Services.Identity;
using ReservationSystem.Reservations.Api.Contracts.Reservations;
using ReservationSystem.Reservations.Infrastructure.EntityFramework;

namespace ReservationSystem.Reservations.Infrastructure.Queries.GetReservationDetails
{
    public class Handler : IRequestHandler<GetReservationDetailsQuery, ReservationDetails>
    {
        private readonly IIdentityContext _identityContext;
        private readonly ReservationContext _reservationContext;
        public Handler(IIdentityContext identityContext, ReservationContext reservationContext)
        {
            _identityContext = identityContext;
            _reservationContext = reservationContext;
        }

        public async Task<ReservationDetails> Handle(GetReservationDetailsQuery request, CancellationToken cancellationToken)
        {
            var reservationDetails = await _reservationContext.Reservations.FindAsync(request.ReservationId, cancellationToken);

            if (reservationDetails.UserId != _identityContext.UserId)
            {
                throw new UnauthorizedOperationException(nameof(GetReservationDetailsQuery), _identityContext.UserId, reservationDetails.UserId.ToString());
            }

            return new ReservationDetails
            {
                ReservationId = reservationDetails.ReservationId
            };
        }
    }
}
