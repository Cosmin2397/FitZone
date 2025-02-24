using FitZone.SubscriptionService.Features.Payment.GetPayments;
using FitZone.SubscriptionService.Shared.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace FitZone.SubscriptionService.Features.Subscription.GetSubscriptions
{
    public class GetSubscriptionsHandler(AppDbContext db)
    : IRequestHandler<GetSubscriptionQuery, List<SubscriptionResponse>>
    {
        public async Task<List<SubscriptionResponse>> Handle(GetSubscriptionQuery request, CancellationToken cancellationToken)
        {
            var subscriptions = await db.Subscriptions
                .AsNoTracking()
                .Include(s => s.SubscriptionPayments) 
                .Include(pt => pt.PersonalTrainerSubscription)
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
