using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using Moq;
using ReservationSystem.Base.Services.Identity;
using ReservationSystem.Base.Services.Outbox;
using ReservationSystem.Catalog.Api.Contracts.Services.Events;
using ReservationSystem.Catalog.Core.Application.Services;
using ReservationSystem.Catalog.Core.Infrastructure.EntityFramework.Entities;
using Xunit;

namespace ReservationSystem.Catalog.Tests.AddService
{
    public class AddServiceTests : TestsBase
    {
        private readonly IServicesCommandService _servicesCommandService;
        private readonly Mock<IOutboxMessagePreparation> _outboxMock;
        private readonly Mock<IIdentityContext> _identityContextMock;
        private readonly Mock<IValidatorFactory> _validatorFactoryMock;

        private readonly IServicesQueryService _servicesQueryService;

        public AddServiceTests()
        {
            ConfigureDbContextInMemmory();

            _outboxMock = new Mock<IOutboxMessagePreparation>();
            _identityContextMock = new Mock<IIdentityContext>();
            _validatorFactoryMock = new Mock<IValidatorFactory>();

            _servicesCommandService = new ServicesCommandService(
                DbContext,
                _identityContextMock.Object,
                _outboxMock.Object,
                _validatorFactoryMock.Object
                );

            _servicesQueryService = new ServicesQueryService(DbContext);
        }

        [Fact]
        public async Task Should_AddService()
        {
            var category = new Category("categoryName", createdBy: Guid.NewGuid());
            var categoryId = (await AddCategoryAsync(category)).CategoryId;

            var addServiceCommand = new Core.Application.Services.Commands.AddService.AddServiceCommand(categoryId,
                "shortDescription", "veryLongDescription", "title", 1.00m, "PLN", true);

            var serviceId = await _servicesCommandService.AddService(addServiceCommand);

            var serviceDetails = await _servicesQueryService.GetServiceDetails(new Core.Application.Services.Queries.GetServiceDetailsQuery(serviceId.Value));

            serviceDetails.ServiceId.Should().Be(serviceId.Value);
        }

        [Fact]
        public async Task Should_PublishEvent()
        {
            var category = new Category("categoryName", createdBy: Guid.NewGuid());
            var categoryId = (await AddCategoryAsync(category)).CategoryId;

            await _servicesCommandService.AddService(new
                Core.Application.Services.Commands.AddService.AddServiceCommand(categoryId,
                "shortDescription", "veryLongDescription", "title", 1.00m, "PLN", true));

            _outboxMock.Verify(p => p.PrepareMessageToPublishAsync(It.IsAny<ServiceAddedEvent>(), It.IsAny<string>(), It.IsAny<CancellationToken>() ));
        }
    }
}
