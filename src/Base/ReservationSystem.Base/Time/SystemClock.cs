using System;

namespace ReservationSystem.Base.Time
{
    public class SystemClock : IClock
    {
        public DateTime Now => DateTime.Now;

        public DateTime UtcNow => DateTime.UtcNow;
    }
}