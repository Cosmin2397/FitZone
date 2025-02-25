using MediatR;
using System.Net;

namespace FitZone.SubscriptionService.Features.Subscription.GetSubscriptions
{
    public record GetSubscriptionQuery() : IRequest<List<SubscriptionResponse>>;
}
