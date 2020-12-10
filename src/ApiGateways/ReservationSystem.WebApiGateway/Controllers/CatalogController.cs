using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.WebApp.ApiGateway.Contracts.Availability;
using ReservationSystem.WebApp.ApiGateway.Contracts.Catalog;
using ReservationSystem.WebApp.ApiGateway.Gateways;

namespace ReservationSystem.WebApp.ApiGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogGateway _catalogGateway;

        public CatalogController(ICatalogGateway catalogGateway)
        {
            _catalogGateway = catalogGateway;
        }

        [HttpGet]
        [Route("services")]
        [ProducesResponseType(typeof(Services), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Services>> GetServices([FromQuery] GetServicesQuery query, CancellationToken cancellationToken)
        {
            return await _catalogGateway.GetServices(query, cancellationToken);
        }

        [HttpGet]
        [Route("services/{serviceId}", Name = nameof(GetServiceDetails))]
        [ProducesResponseType(typeof(ServiceDetails), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ServiceDetails>> GetServiceDetails([FromRoute] int serviceId, CancellationToken cancellationToken)
        {
            return await _catalogGateway.GetServiceDetails(serviceId, cancellationToken);
        }

        [HttpPost]
        [Route("services")]
        [ProducesResponseType(typeof(ServiceDetails), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AddedServiceId>> AddService([FromBody] AddServiceBody addServiceBody, CancellationToken cancellationToken)
        {
            var addedService = await _catalogGateway.AddService(addServiceBody, cancellationToken);

            return CreatedAtRoute(nameof(GetServiceDetails), addedService.Value, addedService);
        }

        [HttpGet]
        [Route("services/{serviceId}/available-dates")]
        [ProducesResponseType(typeof(AvailableDates), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AvailableDates>> GetAvailableDates([FromQuery] GetAvailableDatesQuery query, CancellationToken cancellationToken)
        {
            return await _catalogGateway.GetAvailableDates(query, cancellationToken);
        }

        [HttpGet]
        [Route("categories/{categoryId}", Name = nameof(GetCategoryDetails))]
        [ProducesResponseType(typeof(CategoryDetails), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CategoryDetails>> GetCategoryDetails([FromRoute] int categoryId, CancellationToken cancellationToken)
        {
            return await _catalogGateway.GetCategoryDetails(categoryId, cancellationToken);
        }

        [HttpPost]
        [Route("categories")]
        [ProducesResponseType(typeof(AddedCategoryId), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> AddCategory([FromBody] AddCategoryBody body, CancellationToken cancellationToken)
        {
            var addedCategory = await _catalogGateway.AddCategory(body, cancellationToken);

            return CreatedAtRoute(nameof(GetCategoryDetails), addedCategory.Value, addedCategory);
        }
    }
}