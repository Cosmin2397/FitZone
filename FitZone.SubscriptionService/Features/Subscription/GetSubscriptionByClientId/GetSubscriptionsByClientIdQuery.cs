using FitZone.SubscriptionService.Features.Subscription.GetSubscriptions;
using MediatR;

namespace FitZone.SubscriptionService.Features.Subscription.GetSubscriptionByClientId
{
    public record GetSubscriptionByClientIdQuery(Guid clientId) : IRequest<List<SubscriptionResponse>>;
}
