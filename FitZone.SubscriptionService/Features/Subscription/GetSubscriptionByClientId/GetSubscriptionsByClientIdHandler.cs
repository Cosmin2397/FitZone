using FitZone.SubscriptionService.Features.Payment.GetPayments;
using FitZone.SubscriptionService.Features.Subscription.GetGymSubscriptionsByPeriod;
using FitZone.SubscriptionService.Features.Subscription.GetSubscriptions;
using FitZone.SubscriptionService.Shared.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitZone.SubscriptionService.Features.Subscription.GetSubscriptionByClientId
{
    public class GetSubscriptionsByClientIdHandler(AppDbContext db)
   : IRequestHandler<GetSubscriptionByClientIdQuery, List<SubscriptionResponse>>
    {
        public async Task<List<SubscriptionResponse>> Handle(GetSubscriptionByClientIdQuery request, CancellationToken cancellationToken)
        {
            var subscriptions = await db.Subscriptions
                .AsNoTracking()
                .Include(s => s.SubscriptionPayments)
                .Include(pt => pt.PersonalTrainerSubscription)
                .Where(g => g.ClientId == request.clientId)
                .ToListAsync(cancellationToken);

            var result = subscriptions.Select(subscription => new SubscriptionResponse(
                subscription.Id,
                subscription.GymId,
                subscription.ClientId,
                subscription.Type.ToString(),
                subscription.ClientType.ToString(),
                subscription.Status.ToString(),
                subscription.StartingDate,
                subscription.EndDate,
                subscription.SubscriptionPayments.Select(payment => new PaymentResponse(
                    payment.Id,
                    payment.SubscriptionId,
                    payment.Status.ToString(),
                    payment.Type.ToString(),
                    payment.Amount,
                    payment.PaymentDate
                )).ToList(),
                subscription.PersonalTrainerSubscription
            )).ToList();

            return result;
        }
    }
}
