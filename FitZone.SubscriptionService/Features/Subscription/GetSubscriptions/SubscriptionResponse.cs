using FitZone.SubscriptionService.Features.Payment.GetPayments;
using FitZone.SubscriptionService.Shared.Domain.Entities;

namespace FitZone.SubscriptionService.Features.Subscription.GetSubscriptions
{
    public sealed record SubscriptionResponse(Guid id, Guid clientId, Guid gymId, string subscriptionType, string clientType, string status, DateTime startingDate, DateTime endDate, List<PaymentResponse> payments, PersonalTrainerSubscription ptSub);
}
