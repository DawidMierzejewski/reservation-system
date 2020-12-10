using Microsoft.Extensions.Logging;
using ReservationSystem.Base.Services.MediatR.Behaviors;
using ReservationSystem.Reservations.Infrastructure.EntityFramework;

namespace ReservationSystem.Reservations.Infrastructure
{
    public class ReservationTransactionBehaviour<TRequest, TResponse> : TransactionBehaviour<TRequest, TResponse>
    {
        public ReservationTransactionBehaviour(ReservationContext reservationContext,
            ILogger<TransactionBehaviour<TRequest, TResponse>> logger) : base(reservationContext, logger)
        {

        }
    }
}
