using System.Threading;
using System.Threading.Tasks;

namespace ReservationSystem.Base.Services.Outbox
{
    public interface IOutboxMessagePreparation
    {
        Task PrepareMessageToPublishAsync<T>(T message, string objectId, CancellationToken cancellationToken = default);
    }
}
