using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using ReservationSystem.Base.Services.Identity;
using ReservationSystem.Base.Services.Outbox;
using ReservationSystem.Catalog.Api.Contracts.Services.Events;
using ReservationSystem.Catalog.Core.Application.Services.Commands.AddService;
using ReservationSystem.Catalog.Core.Infrastructure.EntityFramework;
using ReservationSystem.Catalog.Core.Infrastructure.EntityFramework.Entities;

namespace ReservationSystem.Catalog.Core.Application.Services
{
    public class ServicesCommandService : CommandServiceBase, IServicesCommandService
    {
        private readonly CatalogDbContext _dbContext;
        
        private readonly IOutboxMessagePreparation _outbox;

        private readonly IIdentityContext _identityContext;

        public ServicesCommandService(CatalogDbContext dbContext, 
            IIdentityContext identityContext,
            IOutboxMessagePreparation outbox,
            IValidatorFactory validatorFactory)
            : base(validatorFactory)
        {
            _dbContext = dbContext;
            _identityContext = identityContext;
            _outbox = outbox;
        }

        public async Task<ServiceId> AddService(AddServiceCommand command, CancellationToken cancellationToken = default)
        {
            ValidateCommand(command);

            await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

            var addedService = await _dbContext.AddAsync(new Service(
                 command.CategoryId, 
                 command.ShortDescription,
                 command.LongDescription,
                 command.Title,
                 command.InitialPrice,
                 command.CurrencyCode,
                 command.CanBeReserved,
                 _identityContext.UserId), cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            var serviceId = addedService.Entity.ServiceId;

            var @event = new ServiceAddedEvent(serviceId, command.CanBeReserved);

            await _outbox.PrepareMessageToPublishAsync(@event, @event.ObjectId, cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return new ServiceId
            {
                Value = serviceId
            };
        }
    }
}