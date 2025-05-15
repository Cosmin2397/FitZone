using FitZone.Client.Shared.DTOs.Subscription;

namespace FitZone.Client.Shared.Services.Interfaces
{
    public interface ISubscriptionService
    {
        Task<SubscriptionDto> GetSubscriptionByClientId(string id);

        Task<List<GymDetailDto>> GetGyms();

        Task<Guid> AddSubscription(AddSubscriptionRequest subscription);
    }
}
