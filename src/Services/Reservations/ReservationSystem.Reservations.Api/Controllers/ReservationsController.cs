using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Reservations.Api.Contracts.Reservations;
using ReservationSystem.Reservations.Application.Commands.CancelReservation;
using ReservationSystem.Reservations.Application.Commands.ReserveService;
using ReservationSystem.Reservations.Infrastructure.Queries.GetReservationDetails;
using ReservationSystem.Reservations.Infrastructure.Queries.GetReservations;
using ReservationDetails = ReservationSystem.Reservations.Api.Contracts.Reservations.ReservationDetails;

namespace ReservationSystem.Reservations.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReservationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(Contracts.Reservations.Reservations), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Contracts.Reservations.Reservations>> GetReservations(CancellationToken cancellationToken)
        {
            var reservations = await _mediator.Send(
                new GetReservationsQuery(),
                cancellationToken);

            return Ok(reservations);
        }

        [HttpGet]
        [Route("{reservationId}", Name = nameof(GetReservationDetails))]
        [ProducesResponseType(typeof(ReservationDetails), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ReservationDetails>> GetReservationDetails([FromRoute] long reservationId, CancellationToken cancellationToken)
        {
            var reservationDetails = await _mediator.Send(
                new GetReservationDetailsQuery(reservationId), 
                cancellationToken);

            return Ok(reservationDetails);
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(ReservedService), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ReservedService>> ReserveService([FromBody] ReserveServiceBody reserveServiceBody, CancellationToken cancellationToken)
        {
            var reservationId = await _mediator.Send(
                new ReserveServiceCommand(
                    reserveServiceBody.ServiceId,
                    reserveServiceBody.AvailableTermId, 
                    reserveServiceBody.Price, 
                    reserveServiceBody.CurrencyCode), 
                cancellationToken);

            return CreatedAtRoute(nameof(GetReservationDetails), new ReservedService { ReservationId = reservationId.Value });
        }

        [HttpDelete]
        [Route("{reservationId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> CancelReservation(long reservationId, CancellationToken cancellationToken)
        {
            await _mediator.Send(
                new CancelReservationCommand(reservationId), 
                cancellationToken);

            return Ok();
        }
    }
}