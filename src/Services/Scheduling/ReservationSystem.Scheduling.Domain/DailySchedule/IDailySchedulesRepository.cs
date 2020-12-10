using System;
using System.Threading;
using System.Threading.Tasks;

namespace ReservationSystem.Scheduling.Domain.DailySchedule
{
    public interface IDailySchedulesRepository
    {
        Task<DailySchedule> GetDailySchedule(Guid dailyScheduleId, CancellationToken cancellationToken = default);

        Task<DailySchedule> GetDailySchedule(Base.Domain.ValueObjects.Day.Date day, long serviceId, CancellationToken cancellationToken = default);

        Task<DailySchedule> GetDailyScheduleContainsOf(Guid scheduledDate, CancellationToken cancellationToken = default);

        Task Save(DailySchedule dailySchedule, CancellationToken cancellationToken = default);

        Task Update(DailySchedule dailySchedule, CancellationToken cancellationToken = default);
    }
}
