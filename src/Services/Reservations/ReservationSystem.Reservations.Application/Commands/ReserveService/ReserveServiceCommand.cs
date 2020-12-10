using System;
using MediatR;
using ReservationSystem.Base.Services.CQRS;

namespace ReservationSystem.Reservations.Application.Commands.ReserveService
{
    public class ReserveServiceCommand : CommandBase, IRequest<ReservationId>
    {
        public long ServiceId { get; set; }

        public Guid AvailableDateId { get; set; }

        public SeenOffer SeenOffer { get; set; }

        public ReserveServiceCommand(long serviceId, Guid availableTermId, decimal price, string currencyCode)
        {
            ServiceId = serviceId;
            AvailableDateId = availableTermId;
            SeenOffer = new SeenOffer
            {
                CurrencyCode = currencyCode,
                Price = price
            };
        }
    }
}
