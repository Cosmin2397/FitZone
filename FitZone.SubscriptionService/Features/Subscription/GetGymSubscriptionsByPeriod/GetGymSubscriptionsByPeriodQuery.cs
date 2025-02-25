using FitZone.SubscriptionService.Features.Subscription.GetSubscriptions;
using MediatR;

namespace FitZone.SubscriptionService.Features.Subscription.GetGymSubscriptionsByPeriod
{
    public record GetGymSubscriptionsByPeriodQuery(Guid gymId, DateTime startDate, DateTime endDate) : IRequest<List<SubscriptionResponse>>;
}
