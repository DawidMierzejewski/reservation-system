using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Base.Domain.ValueObjects.Day;
using ReservationSystem.Scheduling.Domain.DailySchedule;
using ReservationSystem.Scheduling.Infrastructure.EntityFramework;

namespace ReservationSystem.Scheduling.Infrastructure.Repositories
{
    public class EfDailySchedulesRepository : IDailySchedulesRepository
    {
        private readonly SchedulingContext _schedulingContext;

        public EfDailySchedulesRepository(SchedulingContext schedulingContext)
        {
            _schedulingContext = schedulingContext;
        }

        public async Task<DailySchedule> GetDailySchedule(Guid dailyScheduleId, CancellationToken cancellationToken = default)
        {
            return await _schedulingContext.DailySchedule
                .SingleOrDefaultAsync(s => s.ScheduleId == dailyScheduleId,
                cancellationToken);
        }

        public async Task<DailySchedule> GetDailySchedule(Date day, long serviceId, CancellationToken cancellationToken = default)
        {
            return await _schedulingContext.DailySchedule
                .SingleOrDefaultAsync(s => 
                        s.Day.Day == day.Day &&
                        s.Day.Month == day.Month &&
                        s.Day.Year == day.Year &&
                        s.ServiceId == serviceId,
                    cancellationToken);
        }

        public async Task<DailySchedule> GetDailyScheduleContainsOf(Guid scheduledDate, CancellationToken cancellationToken = default)
        {
            return await _schedulingContext.DailySchedule
                .Where(s => s.Dates.Any(d => d.DateId == scheduledDate))
                .SingleOrDefaultAsync(cancellationToken);
        }

        public async Task Save(DailySchedule dailySchedule, CancellationToken cancellationToken = default)
        {
            await _schedulingContext.DailySchedule.AddAsync(dailySchedule, cancellationToken);
            await _schedulingContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(DailySchedule dailySchedule, CancellationToken cancellationToken = default)
        {
            _schedulingContext.DailySchedule.Update(dailySchedule);
            await _schedulingContext.SaveChangesAsync(cancellationToken);
        }
    }
}
