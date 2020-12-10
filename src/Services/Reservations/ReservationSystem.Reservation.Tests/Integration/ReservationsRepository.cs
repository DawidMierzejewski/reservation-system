using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ReservationSystem.Reservations.Domain.Reservations;
using ReservationSystem.Reservations.Infrastructure.EntityFramework;
using ReservationSystem.Reservations.Infrastructure.Repositories;
using System.Threading.Tasks;
using ReservationSystem.Reservations.Tests.TestDataBuilders;
using Xunit;

namespace ReservationSystem.Reservations.Tests.Integration
{
    public class ReservationsRepository
    {
        private readonly IReservationsRepository _repository;

        public ReservationsRepository()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var dbContextOptions = new DbContextOptionsBuilder<ReservationContext>();
            dbContextOptions.UseSqlServer(config.GetConnectionString("Reservations"), providerOptions => providerOptions.CommandTimeout(60));

            var dbContext = new ReservationContext(dbContextOptions.Options);
            _repository = new EfReservationsRepository(dbContext);
        }

        [Fact]
        public async Task Should_BeAbleToSaveAndLoadService()
        {
            var reservation = new ReservationBuilder()
                .BuildDefault();

            await _repository.SaveAsync(reservation);

            var foundReservation = await _repository
                .GetAsync(reservation.ReservationId);

            foundReservation.ReservationId
                .Should()
                .Be(reservation.ReservationId);

            foundReservation.ServiceId.Should().Be(reservation.ServiceId);
            foundReservation.UserId.Should().Be(reservation.UserId);
            foundReservation.ReservationDate.EndDateTime.Should().Be(reservation.ReservationDate.EndDateTime);
            foundReservation.ReservationDate.StartDateTime.Should().Be(reservation.ReservationDate.StartDateTime);
        }
    }
}