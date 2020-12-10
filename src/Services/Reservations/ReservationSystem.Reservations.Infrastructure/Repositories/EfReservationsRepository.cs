using Microsoft.EntityFrameworkCore;
using ReservationSystem.Reservations.Domain.Reservations;
using ReservationSystem.Reservations.Infrastructure.EntityFramework;
using System;
using System.Threading;
using System.Threading.Tasks;
using ReservationSystem.Base.Domain.Attributes;

namespace ReservationSystem.Reservations.Infrastructure.Repositories
{
    [Repository]
    public class EfReservationsRepository : IReservationsRepository
    {
        private readonly ReservationContext _context;

        public EfReservationsRepository(ReservationContext context)
        {
            _context = context;
        }

        public async Task<Reservation> GetAsync(long reservationId, CancellationToken cancellationToken = default)
        {
            return await _context.Reservations.SingleOrDefaultAsync(r => r.ReservationId == reservationId, cancellationToken);
        }

        public async Task<Reservation> GetByDate(Guid dateId, CancellationToken cancellationToken = default)
        {
            return await _context.Reservations
                .SingleOrDefaultAsync(r => r.ReservationDate.DateId == dateId, cancellationToken);
        }

        public async Task SaveAsync(Reservation reservation, CancellationToken cancellationToken = default)
        {
            await _context.Reservations.AddAsync(reservation, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Reservation reservation, CancellationToken cancellationToken = default)
        {
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
