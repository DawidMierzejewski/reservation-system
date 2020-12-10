using ReservationSystem.Base.Services.Outbox.EntityFramework;
using System.Threading;
using System.Threading.Tasks;

namespace ReservationSystem.Base.Services.Outbox
{
    public interface IOutboxSender
    {
        Task<MessagePublicationResult> PublishUnsentMessagesAsync(int count, CancellationToken cancellationToken = default);
    }
}
