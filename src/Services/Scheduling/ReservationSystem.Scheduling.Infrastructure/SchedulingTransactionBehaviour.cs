using Microsoft.Extensions.Logging;
using ReservationSystem.Base.Services.MediatR.Behaviors;
using ReservationSystem.Scheduling.Infrastructure.EntityFramework;

namespace ReservationSystem.Scheduling.Infrastructure
{
    public class SchedulingTransactionBehaviour<TRequest, TResponse> : TransactionBehaviour<TRequest, TResponse>
    {
        public SchedulingTransactionBehaviour(SchedulingContext reservationContext,
            ILogger<TransactionBehaviour<TRequest, TResponse>> logger) : base(reservationContext, logger)
        {

        }
    }
}
