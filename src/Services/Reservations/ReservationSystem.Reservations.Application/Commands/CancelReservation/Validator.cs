using FluentValidation;

namespace ReservationSystem.Reservations.Application.Commands.CancelReservation
{
    //TODO validation
    public class Validator : AbstractValidator<CancelReservationCommand>
    {
        public Validator()
        {
            RuleFor(p => p.ReservationId).NotEmpty();
        }
    }
}
