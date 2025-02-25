using FitZone.SubscriptionService.Features.Payment.AddPayment;
using FitZone.SubscriptionService.Features.Payment.GetPayments;

namespace FitZone.SubscriptionService.Features.Subscription.AddSubscription
{
    public sealed record AddPtSubscriptionRequest(Guid clientId, Guid gymId, DateTime startingDate, Guid ptId, AddPaymentRequest payment);
}
