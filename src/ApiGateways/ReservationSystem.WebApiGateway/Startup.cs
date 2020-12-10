using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;
using ReservationSystem.Catalog.Api.Contracts;
using ReservationSystem.Reservations.Api.Contracts;
using ReservationSystem.Scheduling.Api.Contracts;
using ReservationSystem.WebApp.ApiGateway.Configuration;
using ReservationSystem.WebApp.ApiGateway.Gateways;

namespace ReservationSystem.WebApp.ApiGateway
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

            ConfigureGateways(services);

            ConfigureApiClients(services);
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

        private void ConfigureGateways(IServiceCollection services)
        {
            services.AddScoped<IReservationsGateway, ReservationsGateway>();
            services.AddScoped<ICatalogGateway, CatalogGateway>();
        }

        private void ConfigureApiClients(IServiceCollection services)
        {
            var connection = Configuration.GetSection("ExternalServices").Get<ExternalServices>();

            services.AddRefitClient<IReservationsApiClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(connection.ReservationUrl));

            services.AddRefitClient<ICatalogApiClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(connection.CatalogUrl));

            services.AddRefitClient<ISchedulingApiClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(connection.SchedulingUrl));
        }
    }
}
