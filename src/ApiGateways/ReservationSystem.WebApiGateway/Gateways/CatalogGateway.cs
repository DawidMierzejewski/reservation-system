using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ReservationSystem.Base.Extensions;
using ReservationSystem.Catalog.Api.Contracts;
using ReservationSystem.Scheduling.Api.Contracts;
using ReservationSystem.Scheduling.Api.Contracts.AvailableDates;
using ReservationSystem.WebApp.ApiGateway.Contracts.Availability;
using ReservationSystem.WebApp.ApiGateway.Contracts.Catalog;
using AvailableDates = ReservationSystem.WebApp.ApiGateway.Contracts.Availability.AvailableDates;
using ServiceDetails = ReservationSystem.WebApp.ApiGateway.Contracts.Catalog.ServiceDetails;
using ServiceItem = ReservationSystem.WebApp.ApiGateway.Contracts.Catalog.ServiceItem;
using Services = ReservationSystem.WebApp.ApiGateway.Contracts.Catalog.Services;

namespace ReservationSystem.WebApp.ApiGateway.Gateways
{
    public class CatalogGateway : ICatalogGateway
    {
        private readonly ICatalogApiClient _catalogApiClient;

        private readonly ISchedulingApiClient _schedulingApiClient;

        public CatalogGateway(ICatalogApiClient catalogApiClient, ISchedulingApiClient schedulingApiClient)
        {
            _catalogApiClient = catalogApiClient;
            _schedulingApiClient = schedulingApiClient;
        }

        public async Task<ServiceDetails> GetServiceDetails(long serviceId, CancellationToken cancellationToken)
        {
            var serviceDetails = await _catalogApiClient.GetServiceDetails(serviceId, cancellationToken);
            if (serviceDetails == null)
            {
                return null;
            }

            return new ServiceDetails
            {
                ServiceId = serviceDetails.ServiceId,
                CanBeReserved =serviceDetails.CanBeReserved,
                CategoryId = serviceDetails.CategoryId,
                CurrencyCode = serviceDetails.CurrencyCode,
                InitialPrice = serviceDetails.InitialPrice,
                LongDescription = serviceDetails.LongDescription,
                ShortDescription = serviceDetails.ShortDescription,
                Title = serviceDetails.Title
            };
        }

        public async Task<Services> GetServices(GetServicesQuery query, CancellationToken cancellationToken = default)
        {
            var services = await _catalogApiClient.GetServices(
                new Catalog.Api.Contracts.Services.GetServicesQuery
                {
                    CategoryId = query.CategoryId
                }, cancellationToken);

            if (services == null || services.Items.IsNullOrEmpty())
            {
                return new Services();
            }

            var items = services.Items.Select(item => new ServiceItem
            {
                InitialPrice = item.InitialPrice,
                LongDescription = item.LongDescription,
                ServiceId = item.ServiceId,
                ShortDescription = item.ShortDescription
            });

            return new Services
            {
                Items = items
            };
        }

        public async Task<AvailableDates> GetAvailableDates(GetAvailableDatesQuery query, CancellationToken cancellationToken = default)
        {
            var availableDates = await _schedulingApiClient.GetAvailableDates(
                new GetAvailableDatesQueryString
                {
                    ServiceId = query.ServiceId,
                    FromDateTime = query.FromDateTime,
                    ToDateTime = query.ToDateTime
                }, cancellationToken);

            if (availableDates == null || availableDates.Items.IsNullOrEmpty())
            {
                return new AvailableDates();
            }

            var items = availableDates.Items.Select(item => new AvailableDate
            {
                DateId = item.DateId,
                Day = item.Day,
                EndTime = item.EndTime,
                Month = item.Month,
                StartTime = item.StartTime,
                Year = item.Year
            });

            return new AvailableDates
            {
                Items = items
            };
        }

        public async Task<AddedCategoryId> AddCategory(AddCategoryBody command, CancellationToken cancellationToken = default)
        {
            var addedCategory = await _catalogApiClient.AddCategory(new Catalog.Api.Contracts.Category.AddCategoryBody
            {
                CategoryName = command.CategoryName
            }, cancellationToken);

            return new AddedCategoryId
            {
                Value = addedCategory.Value
            };
        }

        public async Task<CategoryDetails> GetCategoryDetails(long categoryId, CancellationToken cancellationToken = default)
        {
            var categoryDetails = await _catalogApiClient.GetCategoryDetails(categoryId, cancellationToken);

            return new CategoryDetails
            {
                CategoryId = categoryDetails.CategoryId,
                Name = categoryDetails.Name
            };
        }

        public async Task<AddedServiceId> AddService(AddServiceBody command, CancellationToken cancellationToken = default)
        {
            var addedService = await _catalogApiClient.AddService(new Catalog.Api.Contracts.Services.AddServiceBody
            {
                CategoryId = command.CategoryId,
                ShortDescription = command.ShortDescription,
                LongDescription = command.LongDescription,
                Title = command.Title,
                InitialPrice = command.InitialPrice,
                CurrencyCode = command.CurrencyCode,
                CanBeReserved = command.CanBeReserved
            }, cancellationToken);

            return new AddedServiceId
            {
                Value = addedService.ServiceId
            };
        }
    }
}