using FitZone.SubscriptionService.Features.Payment.AddPayment;
using FitZone.SubscriptionService.Features.Payment.GetPayments;

namespace FitZone.SubscriptionService.Features.Subscription.AddSubscription
{
    public sealed record AddSubscriptionRequest(Guid clientId, Guid gymId, string subscriptionType, string clientType, DateTime startingDate, AddPaymentRequest payment);
}
