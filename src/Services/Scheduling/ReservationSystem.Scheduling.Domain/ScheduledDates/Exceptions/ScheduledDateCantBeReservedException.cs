using ReservationSystem.Base.Domain.Exceptions;
using System;

namespace ReservationSystem.Scheduling.Domain.ScheduledDates.Exceptions
{
    public class ScheduledDateCantBeReservedException : DomainException
    {
        public override string Code => "scheduled_date_cant_be_reserved";

        public Guid DateId { get; }

        public ScheduledDateCantBeReservedException(Guid dateId)
        {
            DateId = dateId;
        }
    }
}
