using FitZone.SubscriptionValidationService.DTOs;
using FitZone.SubscriptionValidationService.Models;
using FitZone.SubscriptionValidationService.Protos;

namespace FitZone.SubscriptionValidationService.Services
{
    public interface ISubscriptionValidationService
    {
        Task<bool> ValidateSubscription(ClientsAccess access);
    }
}
