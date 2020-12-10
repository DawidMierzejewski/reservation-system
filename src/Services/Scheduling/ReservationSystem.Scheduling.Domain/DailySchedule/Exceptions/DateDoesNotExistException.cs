using ReservationSystem.Base.Domain.Exceptions;
using System;

namespace ReservationSystem.Scheduling.Domain.DailySchedule.Exceptions
{
    public class DateDoesNotExistException : DomainException
    {
        public override string Code => "date_does_not_exist";

        public Guid DateId { get; set; }

        public DateDoesNotExistException(Guid dateId)
        {
            DateId = dateId;
        }
    }
}
