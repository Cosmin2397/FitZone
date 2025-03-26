using FitZone.EmployeeManagement.Application.Employees.Commands.AddEmployee;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FitZone.EmployeeManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices
            (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });
            services.AddScoped<IRequestHandler<AddEmployeeCommand, AddEmployeeResult>, AddEmployeeHandler>();

            return services;
        }
    }

}
