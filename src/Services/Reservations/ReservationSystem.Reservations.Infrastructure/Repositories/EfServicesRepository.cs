using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Base.Domain.Attributes;
using ReservationSystem.Reservations.Domain.Service;
using ReservationSystem.Reservations.Infrastructure.EntityFramework;

namespace ReservationSystem.Reservations.Infrastructure.Repositories
{
    [Repository]
    public class EfServicesRepository : IServicesRepository
    {
        private readonly ReservationContext _context;

        public EfServicesRepository(ReservationContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Service service, CancellationToken cancellationToken = default)
        {
            await _context.Services.AddAsync(service, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Service> GetAsync(long id, CancellationToken cancellationToken = default)
        {
            return await _context.Services.SingleOrDefaultAsync(s => s.ServiceId == id, cancellationToken);
        }
    }
}
