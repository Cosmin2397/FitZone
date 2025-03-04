using FitZone.SubscriptionValidationService.Models;

namespace FitZone.SubscriptionValidationService.Services
{
    public interface ISubscriptionValidationService
    {
        Task<bool> ValidateSubscription(ClientsAccess access);
    }
}
