using System.Threading;
using System.Threading.Tasks;

namespace ReservationSystem.Reservations.Domain.Service
{
    public interface IServicesRepository
    {
        Task<Service> GetAsync(long id, CancellationToken cancellationToken = default);
        Task AddAsync(Service service, CancellationToken cancellationToken = default);
    }
}
