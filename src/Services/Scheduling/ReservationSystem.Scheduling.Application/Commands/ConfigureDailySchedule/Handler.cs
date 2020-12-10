using MediatR;
using ReservationSystem.Base.Services.Exceptions;
using ReservationSystem.Base.Services.Identity;
using ReservationSystem.Scheduling.Application.Commands.ConfigureDailySchedule.Exceptions;
using ReservationSystem.Scheduling.Domain.DailySchedule;
using ReservationSystem.Scheduling.Domain.ScheduledDates;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ReservationSystem.Base.Services.Events;

namespace ReservationSystem.Scheduling.Application.Commands.ConfigureDailySchedule
{
    public class Handler : IRequestHandler<ConfigureDailyScheduleCommand, DailyScheduleId>
    {
        private readonly IIdentityContext _identityContext;

        private readonly IDailySchedulesRepository _dailySchedulesRepository;

        private readonly IMessagePublisher _messagePublisher;

        public Handler(IIdentityContext identityContext, 
            IDailySchedulesRepository dailySchedulesRepository, 
            IMessagePublisher messagePublisher)
        {
            _identityContext = identityContext;
            _dailySchedulesRepository = dailySchedulesRepository;
            _messagePublisher = messagePublisher;
        }

        public async Task<DailyScheduleId> Handle(ConfigureDailyScheduleCommand request, CancellationToken cancellationToken)
        {
            if (!_identityContext.IsAuthenticated || !_identityContext.IsAdmin)
            {
                throw new UnauthorizedOperationException(nameof(ConfigureDailyScheduleCommand), _identityContext.UserId);
            }

            var day = Base.Domain.ValueObjects.Day.Date.FromDateTime(request.Day);

            var existDailySchedule = await _dailySchedulesRepository
                .GetDailySchedule(day, request.ServiceId, cancellationToken);

            if (existDailySchedule != null)
            {
                throw new DailyScheduleExistsException(existDailySchedule.ScheduleId);
            }

            var dailySchedule = new DailySchedule(day, request.ServiceId);

            var scheduleDates = request.Dates.Select(d =>
                 new ScheduledDate(d.FromDateTime, d.ToDateTime));

            dailySchedule.ScheduleDates(scheduleDates);

            await _dailySchedulesRepository.Save(dailySchedule, cancellationToken);

            await _messagePublisher.PublishAsync(dailySchedule.GetChanges());

            return new DailyScheduleId(dailySchedule.ScheduleId);
        }
    }
}
