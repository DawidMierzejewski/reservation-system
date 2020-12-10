using System;
using System.Threading;
using System.Threading.Tasks;

namespace ReservationSystem.Reservations.Domain.AvailableDates
{
    public interface IAvailableDatesService
    {
        Task<AvailableDate> GetAvailableDate(Guid dateId, CancellationToken cancellationToken);
    }
}
