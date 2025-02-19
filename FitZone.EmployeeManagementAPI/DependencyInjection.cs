using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;

namespace FitZone.EmployeeManagementAPI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {

            return services;
        }

        public static WebApplication UseApiServices(this WebApplication app)
        {

            app.UseExceptionHandler(options => { });
            return app;
        }
    }
}
