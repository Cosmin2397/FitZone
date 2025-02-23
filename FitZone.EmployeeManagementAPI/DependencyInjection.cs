using HealthChecks.UI.Client;
using Marten;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using MediatR;
using FitZone.EmployeeManagement.Application.Dtos;
using Carter;

namespace FitZone.EmployeeManagementAPI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCarter();

            return services;
        }

        public static WebApplication UseApiServices(this WebApplication app)
        {
            app.MapCarter();

            app.UseExceptionHandler(options => { });

            return app;
        }
    }
}
