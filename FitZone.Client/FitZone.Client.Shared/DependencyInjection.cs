using FitZone.Client.Shared.Services;
using FitZone.Client.Shared.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FitZone.Client.Shared
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSharedServices
        (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(sp => new HttpClient
             {
                 BaseAddress = new Uri("http://192.168.1.3:5074")
             });

            services.AddScoped<IAuthentificationService, AuthentificationService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddScoped<ISubscriptionValidationService, SubscriptionValidationService>();
            services.AddScoped<ITrainingsService, TrainingsService>();
            services.AddScoped<StorageService>();
#if ANDROID
#else
            // Dacă NU e Android, adaugă alt singleton sau implementare fallback
            services.AddSingleton<IQrCodeScannerService, FallbackQrCodeScannerService>();
#endif
            return services;
        }
    }
}
