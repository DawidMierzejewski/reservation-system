using System;

namespace ReservationSystem.Base.Time
{
    public interface IClock
    {
        DateTime Now { get; }

        DateTime UtcNow { get; }
    }
}
