using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Catalog.Api.Contracts.Services;
using ReservationSystem.Catalog.Core.Application.Services;
using ReservationSystem.Catalog.Core.Application.Services.Commands.AddService;
using Services = ReservationSystem.Catalog.Api.Contracts.Services.Services;

namespace ReservationSystem.Catalog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServicesCommandService _servicesCommandService;

        private readonly IServicesQueryService _servicesQueryService;

        public ServicesController(IServicesCommandService servicesCommandService, IServicesQueryService servicesQueryService)
        {
            _servicesCommandService = servicesCommandService;
            _servicesQueryService = servicesQueryService;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(Services), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Services>> GetServices([FromQuery] GetServicesQuery query, CancellationToken cancellationToken)
        {
            var services = await _servicesQueryService.GetServices(
                new Core.Application.Services.Queries.GetServicesQuery(query.CategoryId), 
                cancellationToken);

            return Ok(services);
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(ServiceId), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> AddServices([FromBody] AddServiceBody body, CancellationToken cancellationToken)
        {
            var addedServiceId = await _servicesCommandService.AddService(
                new AddServiceCommand(
                    body.CategoryId, 
                    body.ShortDescription, 
                    body.LongDescription, 
                    body.Title, 
                    body.InitialPrice, 
                    body.CurrencyCode,
                    body.CanBeReserved), 
                cancellationToken);

            return CreatedAtRoute("ServiceDetails", addedServiceId.Value, addedServiceId);
        }

        [HttpGet]
        [Route("{serviceId}", Name = "ServiceDetails")]
        [ProducesResponseType(typeof(ServiceDetails), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ServiceDetails>> GetServiceDetails([FromRoute] long serviceId, CancellationToken cancellationToken)
        {
            var services = await _servicesQueryService.GetServiceDetails(
                new Core.Application.Services.Queries.GetServiceDetailsQuery(serviceId),
                cancellationToken);

            return Ok(services);
        }

        [HttpGet]
        [Route("{serviceId}/offer")]
        [ProducesResponseType(typeof(Offer), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Offer>> GetOffer([FromRoute] long serviceId, CancellationToken cancellationToken)
        {
            var offer = await _servicesQueryService.GetOffer(
                new Core.Application.Services.Queries.GetOfferQuery(serviceId), 
                cancellationToken);

            return Ok(offer);
        }
    }
}