using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Scheduling.Api.Contracts.AvailableDates;
using ReservationSystem.Scheduling.Api.Contracts.DailySchedule;
using ReservationSystem.Scheduling.Application.Commands.ConfigureDailySchedule;

namespace ReservationSystem.Scheduling.Api.Controllers
{
    [Route("api/daily-schedules")]
    [ApiController]
    public class DailySchedulesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DailySchedulesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(AvailableDates), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AvailableDates>> GetAvailableDates([FromQuery] GetAvailableDatesQueryString query, CancellationToken cancellationToken)
        {
            var datetime = DateTime.Now;

            var dates = new Application.Commands.ConfigureDailySchedule.DateToSchedule[]
            {
                new Application.Commands.ConfigureDailySchedule.DateToSchedule(datetime, datetime.AddMinutes(10)),
                new Application.Commands.ConfigureDailySchedule.DateToSchedule(datetime.AddMinutes(20), datetime.AddMinutes(30))
            };

            var dailyScheduleId = await _mediator.Send(
                new ConfigureDailyScheduleCommand(dates, datetime, 1),
                cancellationToken);

            return Ok();
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(AvailableDates), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AvailableDates>> ConfigureDailySchedule([FromBody] ConfigureDailyScheduleBody body, CancellationToken cancellationToken)
        {
            var dates = body.Dates.Select(d =>
                new Application.Commands.ConfigureDailySchedule.DateToSchedule(d.FromDateTime, d.ToDateTime));

            var dailyScheduleId = await _mediator.Send(
                new ConfigureDailyScheduleCommand(dates, body.Day, body.ServiceId),
                cancellationToken);

            return CreatedAtRoute(string.Empty, dailyScheduleId);
        }
    }
}