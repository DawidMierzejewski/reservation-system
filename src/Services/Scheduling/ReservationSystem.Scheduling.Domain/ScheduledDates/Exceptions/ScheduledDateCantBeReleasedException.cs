using ReservationSystem.Base.Domain.Exceptions;
using System;

namespace ReservationSystem.Scheduling.Domain.ScheduledDates.Exceptions
{
    public class ScheduledDateCantBeReleasedException : DomainException
    {
        public override string Code => "scheduled_date_cant_be_released";

        public Guid DateId { get; }

        public ScheduledDateCantBeReleasedException(Guid dateId)
        {
            DateId = dateId;
        }
    }
}
