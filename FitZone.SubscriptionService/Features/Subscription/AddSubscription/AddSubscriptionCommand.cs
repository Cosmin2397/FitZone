using FitZone.SubscriptionService.Features.Payment.AddPayment;
using MediatR;

namespace FitZone.SubscriptionService.Features.Subscription.AddSubscription
{
    public sealed record AddSubscriptionCommand(Guid clientId, Guid gymId, string subscriptionType, string clientType, DateTime startingDate, AddPaymentRequest payment): IRequest<Guid>;
}
