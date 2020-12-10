namespace ReservationSystem.Base.Services.MediatR
{
    public static class EmptyResultFactory
    {
        public static EmptyResult Create()
        {
            return new EmptyResult();
        }
    }
}
