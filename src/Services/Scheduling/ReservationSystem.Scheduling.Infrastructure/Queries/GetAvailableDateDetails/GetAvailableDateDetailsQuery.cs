using MediatR;
using ReservationSystem.Scheduling.Api.Contracts.AvailableDates;
using System;

namespace ReservationSystem.Scheduling.Infrastructure.Queries.GetAvailableDateDetails
{
    public class GetAvailableDateDetailsQuery : IRequest<AvailableDateDetails>
    {
        public Guid DateId { get; }

        public GetAvailableDateDetailsQuery(Guid dateId)
        {
            DateId = dateId;
        }
    }
}
