using System;
using FluentValidation.AspNetCore;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;
using ReservationSystem.Base.Services.Configuration.RabbitMq;
using ReservationSystem.Base.Services.Events;
using ReservationSystem.Base.Services.Identity;
using ReservationSystem.Base.Services.MediatR.Behaviors;
using ReservationSystem.Base.Services.Outbox;
using ReservationSystem.Base.Services.Outbox.BackgroundService;
using ReservationSystem.Base.Services.Outbox.EntityFramework;
using ReservationSystem.Base.Services.Outbox.EntityFramework.Entities;
using ReservationSystem.Base.Time;
using ReservationSystem.Catalog.Api.Contracts;
using ReservationSystem.Reservations.Api.Configuration;
using ReservationSystem.Reservations.Application.Commands.ReserveService;
using ReservationSystem.Reservations.Domain.AvailableDates;
using ReservationSystem.Reservations.Domain.Offer;
using ReservationSystem.Reservations.Domain.Reservations;
using ReservationSystem.Reservations.Domain.Service;
using ReservationSystem.Reservations.Infrastructure;
using ReservationSystem.Reservations.Infrastructure.EntityFramework;
using ReservationSystem.Reservations.Infrastructure.Events;
using ReservationSystem.Reservations.Infrastructure.Queries.GetReservations;
using ReservationSystem.Reservations.Infrastructure.Repositories;
using ReservationSystem.Reservations.Infrastructure.Services;
using ReservationSystem.Scheduling.Api.Contracts;
using MassTransitRabbitMqPublisher = ReservationSystem.Base.Services.Outbox.MassTransitRabbitMqPublisher;

namespace ReservationSystem.Reservations.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddMvc().AddFluentValidation(
                fv => fv.RegisterValidatorsFromAssemblyContaining<Application.Commands.CancelReservation.CancelReservationCommand>());

            ConfigureApiClients(services);

            ConfigureEntityFramework(services);

            ConfigureOutbox(services);

            ConfigureMediatR(services);

            Configure(services);

            ConfigureRepositories(services);

            ConfigureExternalServices(services);

            ConfigureRabbitMq(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureApiClients(IServiceCollection services)
        {
            var connection = Configuration.GetSection("ExternalServices").Get<ExternalServices>();

            services.AddRefitClient<ICatalogApiClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(connection.CatalogUrl));

            services.AddRefitClient<ISchedulingApiClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(connection.SchedulingUrl));
        }

        private void ConfigureEntityFramework(IServiceCollection services)
        {
            services.AddDbContext<ReservationContext>(options =>
                     options.UseSqlServer(Configuration.GetConnectionString("Reservations")));
        }

        private void ConfigureOutbox(IServiceCollection services)
        {
            services.AddScoped<Base.Services.Outbox.IBusPublisher, Base.Services.Outbox.MassTransitRabbitMqPublisher>();
            services.AddScoped<IOutboxMessagePreparation, EntityFrameworkOutboxMessagePreparation<ReservationContext, OutboxMessage>>();
            services.AddScoped<IOutboxSender, EntityFrameworkOutboxSender<ReservationContext, OutboxMessage>>();

            services.AddScoped<IMessagePublisher, OutboxMessagePublisher>();

            services.AddHostedService<OutboxBackgroundService>();

            services.AddSingleton(sp => Configuration.GetSection("OutboxConfiguration").Get<OutboxConfiguration>());
        }

        private void ConfigureMediatR(IServiceCollection services)
        {
            services.AddMediatR(typeof(ReserveServiceCommand));
            services.AddMediatR(typeof(GetReservationsQuery));
        }

        private void Configure(IServiceCollection services)
        {
            services.AddScoped<IIntegrationEventsMapper, IntegrationEventsMapper>();

            services.AddScoped<IIdentityContext, DefaultIdentityContext>();

            services.AddScoped<IReservationFactory, ReservationFactory>();

            services.AddScoped<IClock, SystemClock>();

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ReservationTransactionBehaviour<,>));
        }

        private void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<IServicesRepository, EfServicesRepository>();
            services.AddScoped<IReservationsRepository, EfReservationsRepository>();
        }

        private void ConfigureExternalServices(IServiceCollection services)
        {
            services.AddScoped<IAvailableDatesService, AvailableDatesService>();
            services.AddScoped<IOfferService, OfferService>();
        }

        private void ConfigureRabbitMq(IServiceCollection services)
        {
            var connection = Configuration.GetSection("RabbitMq").Get<RabbitMqConfiguration>()?.Connection;
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection), $"{nameof(ConfigureRabbitMq)}, rabbitMq connection is null");
            }

            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(connection.Host, connection.VirtualHost, h =>
                    {
                        h.Username(connection.Username);
                        h.Password(connection.Password);
                    });
                });
            });

            services.AddMassTransitHostedService();
        }
    }
}
