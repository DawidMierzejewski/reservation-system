using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ReservationSystem.Base.Services.Events;
using ReservationSystem.Base.Services.Identity;
using ReservationSystem.Base.Time;
using ReservationSystem.Reservations.Domain.AvailableDates;
using ReservationSystem.Reservations.Domain.Offer;
using ReservationSystem.Reservations.Domain.Reservations;
using ReservationSystem.Reservations.Domain.Service;
using ReservationSystem.Reservations.Application.Commands.ReserveService.Exceptions;

namespace ReservationSystem.Reservations.Application.Commands.ReserveService
{
    public class Handler : IRequestHandler<ReserveServiceCommand, ReservationId>
    {
        private readonly IAvailableDatesService _availableDatesService;
        private readonly IReservationsRepository _reservationsRepository;
        private readonly IOfferService _offerService;
        private readonly IReservationFactory _reservationFactory;
        private readonly IClock _clock;
        private readonly IMessagePublisher _messagePublisher;
        private readonly IIdentityContext _identityContext;
        private readonly IServicesRepository _servicesRepository;

        public Handler(
            IAvailableDatesService availableDatesService,
            IReservationsRepository reservationsRepository,
            IOfferService offerService,
            IReservationFactory reservationFactory,
            IClock clock,
            IMessagePublisher messagePublisher,
            IIdentityContext identityContext,
            IServicesRepository servicesRepository)
        {
            _availableDatesService = availableDatesService;
            _reservationsRepository = reservationsRepository;
            _offerService = offerService;
            _reservationFactory = reservationFactory;
            _clock = clock;
            _messagePublisher = messagePublisher;
            _identityContext = identityContext;
            _servicesRepository = servicesRepository;
        }

        public async Task<ReservationId> Handle(ReserveServiceCommand request, CancellationToken cancellationToken)
        {
            var reservation = await CreateReservation(request, cancellationToken);

            await _reservationsRepository.SaveAsync(reservation, cancellationToken);

            await _messagePublisher.PublishAsync(reservation.GetChanges());

            return new ReservationId
            {
                Value = reservation.ReservationId
            };
        }

        private async Task<Reservation> CreateReservation(ReserveServiceCommand request, CancellationToken cancellationToken)
        {
            var service = await _servicesRepository.GetAsync(request.ServiceId, cancellationToken);
            if (service == null)
            {
                throw new ServiceDoesNotExistException(request.ServiceId);
            }

            var offer = await _offerService.GetOffer(_identityContext.UserId, request.ServiceId, cancellationToken);
            if (!offer.AreSame(new Offer(request.SeenOffer.Price, request.SeenOffer.CurrencyCode)))
            {
                throw new OffersAreNotSameException(offer.Price, offer.CurrencyCode, request.SeenOffer.Price, request.SeenOffer.CurrencyCode);
            }

            var existingReservation = await _reservationsRepository.GetByDate(request.AvailableDateId, cancellationToken);
            if (existingReservation != null)
            {
                throw new AvailableDateIsReserved(request.AvailableDateId);
            }

            var availableDate = await _availableDatesService.GetAvailableDate(request.AvailableDateId, cancellationToken);

            var reservation = _reservationFactory.Create(
                availableDate,
                offer,
                _identityContext.UserId,
                service,
                _clock.UtcNow);

            return reservation;
        }
    }
}
