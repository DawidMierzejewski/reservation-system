using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReservationSystem.Base.Services.MediatR.Behaviors.Attributes;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ReservationSystem.Base.Services.MediatR.Behaviors
{
    public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TransactionBehaviour<TRequest, TResponse>> _logger;
        private readonly DbContext _dbContext;

        public TransactionBehaviour(DbContext dbContext,
            ILogger<TransactionBehaviour<TRequest, TResponse>> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var requestType = request.GetType();
            if (!Attribute.IsDefined(requestType, typeof(ValidationRequestAttribute)))
            {
                return await next();
            }

            var response = default(TResponse);
            Guid transactionId = Guid.Empty;

            try
            {
                await using var transaction = await _dbContext.Database.BeginTransactionAsync();

                transactionId = transaction.TransactionId;

                _logger.LogInformation($"Begin transaction : {transactionId}, {requestType})");

                response = await next();

                _logger.LogInformation($"Commit transaction : {transactionId}, {requestType}");

                await transaction.CommitAsync();

                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Transaction failure : {transactionId}, {requestType}, {response}");

                throw;
            }
        }
    }
}
