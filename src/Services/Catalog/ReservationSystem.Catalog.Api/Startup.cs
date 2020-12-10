using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReservationSystem.Base.Services.Configuration.RabbitMq;
using ReservationSystem.Base.Services.Identity;
using ReservationSystem.Base.Services.Outbox;
using ReservationSystem.Base.Services.Outbox.BackgroundService;
using ReservationSystem.Base.Services.Outbox.EntityFramework;
using ReservationSystem.Base.Services.Outbox.EntityFramework.Entities;
using ReservationSystem.Base.Time;
using ReservationSystem.Catalog.Core.Application.Categories;
using ReservationSystem.Catalog.Core.Application.Services;
using ReservationSystem.Catalog.Core.Infrastructure.EntityFramework;
using System;

namespace ReservationSystem.Catalog.Api
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
                 fv => fv.RegisterValidatorsFromAssemblyContaining<Core.Application.Categories.Commands.AddCategory.AddCategoryCommand>());

            Configure(services);

            ConfigureRabbitMq(services);

            ConfigureEntityFramework(services);

            ConfigureOutbox(services);
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

        private void ConfigureEntityFramework(IServiceCollection services)
        {
            services.AddDbContext<CatalogDbContext>(options =>
                     options.UseSqlServer(Configuration.GetConnectionString("Catalog")));
        }

        private void ConfigureOutbox(IServiceCollection services)
        {
            services.AddScoped<Base.Services.Outbox.IBusPublisher, Base.Services.Outbox.MassTransitRabbitMqPublisher>();
            services.AddScoped<IOutboxMessagePreparation, EntityFrameworkOutboxMessagePreparation<CatalogDbContext, OutboxMessage>>();
            services.AddScoped<IOutboxSender, EntityFrameworkOutboxSender<CatalogDbContext, OutboxMessage>>();

            services.AddHostedService<OutboxBackgroundService>();

            services.AddSingleton(sp => Configuration.GetSection("OutboxConfiguration").Get<OutboxConfiguration>());
        }

        private void Configure(IServiceCollection services)
        {
            services.AddScoped<IIdentityContext, DefaultIdentityContext>();

            services.AddScoped<IClock, SystemClock>();

            services.AddScoped<IServicesCommandService, ServicesCommandService>();

            services.AddScoped<IServicesQueryService, ServicesQueryService>();

            services.AddScoped<ICategoriesCommandService, CategoriesCommandService>();

            services.AddScoped<ICategoriesQueryService, CategoriesQueryService>();
        }
    }
}
