using MassTransit;
using ReservationSystem.Reservations.Api.Contracts.Reservations.Events;
using System.Threading.Tasks;
using ReservationSystem.Scheduling.Domain.DailySchedule;

namespace ReservationSystem.Scheduling.Application.Events.External.Handlers
{
    public class ReservationCreatedHandler : IConsumer<ReservationCreatedEvent>
    {
        private readonly IDailySchedulesRepository _dailySchedulesRepository;

        public ReservationCreatedHandler(IDailySchedulesRepository dailySchedulesRepository)
        {
            _dailySchedulesRepository = dailySchedulesRepository;
        }

        public async Task Consume(ConsumeContext<ReservationCreatedEvent> context)
        {
            var dateId = context.Message.DateId;
            var dailySchedule = await _dailySchedulesRepository.GetDailyScheduleContainsOf(dateId);

            dailySchedule.ReserveDate(dateId);

            await _dailySchedulesRepository.Save(dailySchedule);
        }
    }
}
