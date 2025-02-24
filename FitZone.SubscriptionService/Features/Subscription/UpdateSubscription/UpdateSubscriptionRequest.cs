using FitZone.SubscriptionService.Features.Payment.AddPayment;

namespace FitZone.SubscriptionService.Features.Subscription.UpdateSubscription
{
    public sealed record UpdateSubscriptionRequest(Guid subId, string status, string subscriptionType, string clientType, int addedDays, Guid? personalTrainerID, AddPaymentRequest? payment);
}
