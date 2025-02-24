using FitZone.SubscriptionService.Features.Payment.GetPayments;
using FitZone.SubscriptionService.Features.Subscription.GetSubscriptionByClientId;
using FitZone.SubscriptionService.Features.Subscription.GetSubscriptions;
using FitZone.SubscriptionService.Shared.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitZone.SubscriptionService.Features.Subscription.GetSubscriptionById
{
    public class GetSubscriptionByIdHandler(AppDbContext db)
  : IRequestHandler<GetSubscriptionByIdQuery, SubscriptionResponse>
    {
        public async Task<SubscriptionResponse> Handle(GetSubscriptionByIdQuery request, CancellationToken cancellationToken)
        {
            var subscription = await db.Subscriptions
                .AsNoTracking()
                .Include(s => s.SubscriptionPayments)
                .Include(pt => pt.PersonalTrainerSubscription)
                .Where(g => g.Id == request.id)
                .FirstOrDefaultAsync(cancellationToken);

            if (subscription != null)
            {
                var result = new SubscriptionResponse(
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
                );

                return result;
            }

            return null;
        }
    }
}
