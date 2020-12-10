namespace ReservationSystem.Base.Services.Outbox.BackgroundService
{
    public class OutboxConfiguration
    {
        public int IntervalInSeconds { get; set; } = 30;

        public int MessageCount { get; set; } = 50;
    }
}
