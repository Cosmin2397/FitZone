using FitZone.SubscriptionService.Features.Subscription.GetSubscriptions;
using MediatR;

namespace FitZone.SubscriptionService.Features.Subscription.GetSubscriptionById
{
    public record GetSubscriptionByIdQuery(Guid id) : IRequest<SubscriptionResponse>;
}
