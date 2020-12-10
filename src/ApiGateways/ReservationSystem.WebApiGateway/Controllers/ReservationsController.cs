using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.WebApp.ApiGateway.Contracts.Reservations;
using ReservationSystem.WebApp.ApiGateway.Gateways;

namespace ReservationSystem.WebApp.ApiGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationsGateway _reservationsGateway;
        public ReservationsController(IReservationsGateway reservationsGateway)
        {
            _reservationsGateway = reservationsGateway;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(Contracts.Reservations.Reservations), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Contracts.Reservations.Reservations>> GetReservations(CancellationToken cancellationToken)
        {
            return await _reservationsGateway.GetReservations(cancellationToken);
        }

        [HttpGet]
        [Route("{reservationId}")]
        [ProducesResponseType(typeof(ReservationDetails), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ReservationDetails>> GetReservationDetails([FromRoute] long reservationId, CancellationToken cancellationToken)
        {
            return await _reservationsGateway.GetReservationDetails(reservationId, cancellationToken);
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(ReservedServiceId), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ReservedServiceId>> ReserveService([FromBody] ReserveServiceBody reserveServiceBody, CancellationToken cancellationToken)
        {
            return await _reservationsGateway.ReserveService(reserveServiceBody, cancellationToken);
        }

        [HttpDelete]
        [Route("{reservationId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public Task CancelReservation([FromRoute] long reservationId, CancellationToken cancellationToken)
        {
            return _reservationsGateway.CancelReservation(reservationId, cancellationToken);
        }
    }
}