using FluentAssertions;
using ReservationSystem.Base.Domain.Exceptions;
using ReservationSystem.Reservations.Api.Contracts.Reservations.Events;
using ReservationSystem.Reservations.Domain.AvailableDates;
using ReservationSystem.Reservations.Domain.Reservations;
using ReservationSystem.Reservations.Domain.Reservations.Exceptions;
using ReservationSystem.Reservations.Domain.Service;
using System;
using System.Linq;
using ReservationSystem.Base.Domain.ValueObjects.Day;
using ReservationSystem.Base.Domain.ValueObjects.TimeSlot;
using ReservationSystem.Reservations.Tests.TestDataBuilders;
using Xunit;
using Currency = ReservationSystem.Base.Domain.ValueObjects.Money.Currency;

namespace ReservationSystem.Reservations.Tests.Domain
{
    public class ReservationTests
    {
        private readonly ReservationFactory _reservationFactory;

        public ReservationTests()
        {
            _reservationFactory = new ReservationFactory();
        }

        [Fact]
        public void Reservation_Should_BeCanceled()
        {
            var reservation = GivenReservation()
                .WithStatus(ReservationStatus.Reserved)
                .WithReservationDateTime(
                    new DateTime(2020, 10, 10, 10, 0, 0),
                    new DateTime(2020, 10, 10, 10, 30, 0))
                .Build();

            var date = new DateTime(2020, 10, 10, 9, 30, 0);
            reservation.Cancel(date);

            reservation.ReservationStatus
                .Should()
                .Be(ReservationStatus.Canceled);

            var changes = reservation.GetChanges();

            var count = changes.OfType<ReservationSystem.Reservations.Domain.Reservations.Events.ReservationCancelledEvent>().Count();
            count.Should().Be(1);
        }

        [Fact]
        public void Reservation_Should_NotBeCanceled()
        {
            var reservation = GivenReservation()
                .WithStatus(ReservationStatus.Canceled)
                .Build();

            Action cancelByUser = () => reservation.Cancel(DateTime.Now);

            Assert.Throws<ReservationCantBeCanceledException>(cancelByUser);
        }

        [Fact]
        public void Should_NotCreateReservation_When_AvailableDateIsNotFree()
        {
            var term = GivenTerm()
                .IsFree(false)
                .WithDay(new Date(2020, 01, 01))
                .WithTimeSlot(new TimeSlot(new TimeSpan(10, 0, 0), new TimeSpan(10, 15, 0)))
                .Build();

            Action createReservation = () => CreateReservation(term, DateTime.Now);

            Assert.Throws<AvailableDateIsNotFreeException>(createReservation);
        }

        [Fact]
        public void Should_NotCreateReservation_When_AvailableDateIsPast()
        {
            var term = GivenTerm()
                .IsFree(true)
                .WithDay(new Date(2018, 01, 01))
                .WithTimeSlot(new TimeSlot(new TimeSpan(10, 0, 0), new TimeSpan(10, 15, 0)))
                .Build();

            Action createReservation = () => CreateReservation(term, DateTime.Now);

            Assert.Throws<AvailableDateIsPastException>(createReservation);
        }

        [Fact]
        public void Should_NotCreateReservation_When_AvailableDateIsTooLate()
        {
            var now = DateTime.Now;

            var term = GivenTerm()
                .IsFree(true)
                .WithDay(Date.FromDateTime(now))
                .WithTimeSlot(
                    new TimeSlot(now.AddMinutes(5).TimeOfDay, now.AddMinutes(30).TimeOfDay))
                .Build();

            Action createReservation = () => CreateReservation(term, now);

            Assert.Throws<AvailableDateIsTooLateException>(createReservation);
        }

        [Fact]
        public void Should_CreateReservation()
        {
            var now = DateTime.Now;
            var term = GivenTerm()
                .IsFree(true)
                .WithDay(Date.FromDateTime(now))
                .WithTimeSlot(new TimeSlot(now.AddMinutes(16).TimeOfDay, now.AddMinutes(40).TimeOfDay))
                .Build();

            var userId = Guid.NewGuid();
            var offer = new ReservationSystem.Reservations.Domain.Offer.Offer(1.00M, Currency.PLN.ToString());
            var reservation = CreateReservation(
                term,
                offer,
                new Service(1, true), 
                userId,
                now);

            reservation.ServiceId.Should().Be(1);
            reservation.UserId.Should().Be(userId);
            reservation.Offer.Should().Be(offer);
            reservation.ReservationDate.TimeSlot.Should().Be(term.TimeSlot);
            reservation.ReservationStatus.Should().Be(ReservationStatus.Reserved);
            reservation.ReservationDate.DateId.Should().Be(term.DateId);
        }

        private void CreateReservation(AvailableDate availableDate, DateTime now)
        {
            CreateReservation(availableDate, new Reservations.Domain.Offer.Offer(1.0m, "PLN"), new Service(1,true) , Guid.NewGuid(), now);
        }

        private Reservations.Domain.Reservations.Reservation CreateReservation(AvailableDate term, ReservationSystem.Reservations.Domain.Offer.Offer offer, Service service, Guid userId, DateTime now)
        {
            return _reservationFactory.Create(term, offer, userId, service, now);
        }

        private AvailableDateBuilder GivenTerm()
        {
            return new AvailableDateBuilder();
        }

        private ReservationBuilder GivenReservation()
        {
            return new ReservationBuilder();
        }
    }
}
