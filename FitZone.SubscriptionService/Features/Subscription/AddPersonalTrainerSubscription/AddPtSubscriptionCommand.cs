using FitZone.SubscriptionService.Features.Payment.AddPayment;
using MediatR;

namespace FitZone.SubscriptionService.Features.Subscription.AddSubscription
{
    public sealed record AddPtSubscriptionCommand(Guid clientId, Guid gymId, DateTime startingDate, Guid ptId, AddPaymentRequest payment): IRequest<Guid>;
}
