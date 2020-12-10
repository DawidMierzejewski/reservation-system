using System;
using System.Threading;
using System.Threading.Tasks;

namespace ReservationSystem.Reservations.Domain.Reservations
{
    public interface IReservationsRepository
    {
        Task<Reservation> GetAsync(long reservationId, CancellationToken cancellationToken = default);

        Task SaveAsync(Reservation reservation, CancellationToken cancellationToken = default);

        Task UpdateAsync(Reservation reservation, CancellationToken cancellationToken = default);

        Task<Reservation> GetByDate(Guid dateId, CancellationToken cancellationToken = default);
    }
}
