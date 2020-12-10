using System;

namespace ReservationSystem.Reservations.Application.Commands.ReserveService.Exceptions
{
    public class AvailableDateIsReserved : Base.Services.Exceptions.ApplicationException
    {
        public override string Code => "available_date_is_reserved";

        public Guid AvailableDate { get; set; }

        public AvailableDateIsReserved(Guid availableDate) : base("available_date_is_reserved")
        {
            AvailableDate = availableDate;
        }
    }
}
