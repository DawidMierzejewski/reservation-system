using System.Threading.Tasks;

namespace ReservationSystem.Base.Services.Events
{
    public interface IExternalEventHandler<in TEvent> where TEvent : class, IntegrationEvent
    {
        Task HandleAsync(TEvent @event);
    }
}
