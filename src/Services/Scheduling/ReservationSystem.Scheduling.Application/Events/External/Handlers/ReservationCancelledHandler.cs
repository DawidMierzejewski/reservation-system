using MassTransit;
using ReservationSystem.Reservations.Api.Contracts.Reservations.Events;
using System.Threading.Tasks;
using ReservationSystem.Scheduling.Domain.DailySchedule;

namespace ReservationSystem.Scheduling.Application.Events.External.Handlers
{
    public class ReservationCancelledHandler : IConsumer<ReservationCancelledEvent>
    {
        private readonly IDailySchedulesRepository _dailySchedulesRepository;

        public ReservationCancelledHandler(IDailySchedulesRepository dailySchedulesRepository)
        {
            _dailySchedulesRepository = dailySchedulesRepository;
        }

        public async Task Consume(ConsumeContext<ReservationCancelledEvent> context)
        {
            var dateId = context.Message.DateId;
            var dailySchedule = await _dailySchedulesRepository.GetDailyScheduleContainsOf(dateId);

            dailySchedule.ReleaseDate(dateId);

            await _dailySchedulesRepository.Save(dailySchedule);
        }
    }
}
