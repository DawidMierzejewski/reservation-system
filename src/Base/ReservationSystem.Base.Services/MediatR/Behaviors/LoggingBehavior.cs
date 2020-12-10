using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ReservationSystem.Base.Services.MediatR.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var logGuid = Guid.NewGuid();

            _logger.LogInformation($"Handling command {request.GetType()}, logGuid: {logGuid}", request.GetType(), request);

            var response = await next();

            _logger.LogInformation($"Handled command {request.GetType()}, logGuid: {logGuid}", request.GetType(), response);

            return response;
        }
    }
}
