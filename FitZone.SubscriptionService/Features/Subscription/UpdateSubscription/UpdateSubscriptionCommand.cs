using FitZone.SubscriptionService.Features.Payment.AddPayment;
using MediatR;

namespace FitZone.SubscriptionService.Features.Subscription.UpdateSubscription
{
    public sealed record UpdateSubscriptionCommand(Guid subId, string status, string subscriptionType, string clientType, int addedDays, Guid? personalTrainerID, AddPaymentRequest? payment) : IRequest<bool>;
}
