using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Base.Services.Identity;
using ReservationSystem.Reservations.Api.Contracts.Reservations;
using ReservationSystem.Reservations.Infrastructure.EntityFramework;

namespace ReservationSystem.Reservations.Infrastructure.Queries.GetReservations
{
    public class Handler : IRequestHandler<GetReservationsQuery, Api.Contracts.Reservations.Reservations>
    {
        private readonly IIdentityContext _identityContext;
        private readonly ReservationContext _reservationContext;
        public Handler(IIdentityContext identityContext, ReservationContext reservationContext)
        {
            _identityContext = identityContext;
            _reservationContext = reservationContext;
        }

        public async Task<Api.Contracts.Reservations.Reservations> Handle(GetReservationsQuery request, CancellationToken cancellationToken)
        {
            var reservationDetails = await _reservationContext.Reservations
                           .Where(r => r.UserId == _identityContext.UserId).ToArrayAsync(cancellationToken);

            var reservations = reservationDetails.Select(r => new ReservationItem
            {
                ReservationId = r.ReservationId
            });

            return new Api.Contracts.Reservations.Reservations
            {
                ReservationItems = reservations
            };
        }
    }
}
