using FitZone.Client.Shared.Services;
using FitZone.Client.Shared.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.Client.Shared
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSharedServices
        (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(sp => new HttpClient
             {
                 BaseAddress = new Uri("https://10.0.2.2:7049")
             });

            services.AddScoped<IAuthentificationService, AuthentificationService>();
            services.AddScoped<StorageService>();

            return services;
        }
    }
}
