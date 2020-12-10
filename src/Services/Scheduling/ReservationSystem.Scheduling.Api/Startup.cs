using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReservationSystem.Base.Services.Configuration.RabbitMq;
using ReservationSystem.Base.Services.Outbox;
using System;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Base.Services.Events;
using ReservationSystem.Base.Services.Identity;
using ReservationSystem.Base.Services.MediatR.Behaviors;
using ReservationSystem.Base.Services.Outbox.BackgroundService;
using ReservationSystem.Base.Services.Outbox.EntityFramework;
using ReservationSystem.Base.Services.Outbox.EntityFramework.Entities;
using ReservationSystem.Base.Time;
using ReservationSystem.Scheduling.Infrastructure.EntityFramework;
using ReservationSystem.Scheduling.Infrastructure.Queries.GetAvailableDates;
using ReservationSystem.Scheduling.Application.Commands.ConfigureDailySchedule;
using ReservationSystem.Scheduling.Domain.DailySchedule;
using ReservationSystem.Scheduling.Infrastructure;
using ReservationSystem.Scheduling.Infrastructure.Events;
using ReservationSystem.Scheduling.Infrastructure.Repositories;

namespace ReservationSystem.Scheduling.Api
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
                fv => fv.RegisterValidatorsFromAssemblyContaining<ConfigureDailyScheduleCommand>());

            ConfigureRabbitMq(services);

            ConfigureOutbox(services);

            ConfigureMediatR(services);

            ConfigureEntityFramework(services);

            ConfigureRepositories(services);

            Configure(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
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

        private void ConfigureOutbox(IServiceCollection services)
        {
            services.AddScoped<Base.Services.Outbox.IBusPublisher, Base.Services.Outbox.MassTransitRabbitMqPublisher>();
            services.AddScoped<IOutboxMessagePreparation, EntityFrameworkOutboxMessagePreparation<SchedulingContext, OutboxMessage>>();
            services.AddScoped<IOutboxSender, EntityFrameworkOutboxSender<SchedulingContext, OutboxMessage>>();

            services.AddScoped<IMessagePublisher, OutboxMessagePublisher>();

            services.AddHostedService<OutboxBackgroundService>();

            services.AddSingleton(sp => Configuration.GetSection("OutboxConfiguration").Get<OutboxConfiguration>());
        }

        private void ConfigureMediatR(IServiceCollection services)
        {
            services.AddMediatR(typeof(ConfigureDailyScheduleCommand));
            services.AddMediatR(typeof(GetAvailableDatesQuery));
        }

        private void ConfigureEntityFramework(IServiceCollection services)
        {
            services.AddDbContext<SchedulingContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Scheduling")));
        }

        private void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<IDailySchedulesRepository, EfDailySchedulesRepository>();
        }

        private void Configure(IServiceCollection services)
        {
            services.AddScoped<IIntegrationEventsMapper, IntegrationEventsMapper>();

            services.AddScoped<IIdentityContext, DefaultIdentityContext>();

            services.AddScoped<IClock, SystemClock>();

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(SchedulingTransactionBehaviour<,>));
        }
    }
}
