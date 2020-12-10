namespace ReservationSystem.Base.Services.Outbox.EntityFramework
{
    public class MessagePublicationResult
    {
        public int PublishedMessagesCount { get; set; }

        public int UnpublishedMessagesCount { get; set; }

        public int PublishedByAnotherProcess { get; set; }
    }
}
