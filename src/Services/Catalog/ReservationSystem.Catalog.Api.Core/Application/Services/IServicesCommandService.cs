using System.Threading;
using System.Threading.Tasks;
using ReservationSystem.Catalog.Core.Application.Services.Commands.AddService;

namespace ReservationSystem.Catalog.Core.Application.Services
{
    public interface IServicesCommandService
    {
        Task<ServiceId> AddService(AddServiceCommand command, CancellationToken cancellationToken = default);
    }
}
