namespace ReservationSystem.Base.Services.Events.BusPublisher.RabbitMq
{
    public interface IMessageConfiguration
    {
        string RoutingKey { get; set;  }
        string Exchange { get; set; }
    }
}
