using FluentValidation;

namespace ReservationSystem.Reservations.Application.Commands.ReserveService
{
    //TODO validation
    public class Validator : AbstractValidator<ReserveServiceCommand>
    {
        public Validator()
        {
            RuleFor(p => p.AvailableDateId).NotEmpty();
            RuleFor(p => p.ServiceId).NotEmpty();
            RuleFor(p => p.SeenOffer).NotEmpty();
            RuleFor(p => p.SeenOffer.Price).NotEmpty();
            RuleFor(p => p.SeenOffer.CurrencyCode).NotEmpty();
        }
    }
}
