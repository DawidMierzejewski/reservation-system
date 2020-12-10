using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Scheduling.Api.Contracts.AvailableDates;
using ReservationSystem.Scheduling.Infrastructure.Queries.GetAvailableDateDetails;
using ReservationSystem.Scheduling.Infrastructure.Queries.GetAvailableDates;

namespace ReservationSystem.Scheduling.Api.Controllers
{
    [Route("api/available-dates")]
    [ApiController]
    public class AvailableDatesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AvailableDatesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(AvailableDates), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AvailableDates>> GetAvailableDates([FromQuery] GetAvailableDatesQueryString query, CancellationToken cancellationToken)
        {
            var reservationDetails = await _mediator.Send(
                new GetAvailableDatesQuery(query.ServiceId, query.FromDateTime, query.ToDateTime),
                cancellationToken);

            return Ok(reservationDetails);
        }

        [HttpGet]
        [Route("{dateId}")]
        [ProducesResponseType(typeof(AvailableDateDetails), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AvailableDateDetails>> GetAvailableDateDetails([FromRoute] Guid dateId, CancellationToken cancellationToken)
        {
            var availableDateDetails = await _mediator.Send(
                new GetAvailableDateDetailsQuery(dateId),
                cancellationToken);

            return Ok(availableDateDetails);
        }
    }
}