using FitZone.SubscriptionService.Features.Payment.GetPayments;
using FitZone.SubscriptionService.Shared.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitZone.SubscriptionService.Features.Payment.GetGymPayments
{
    public class GetGymPaymentHandler(AppDbContext db)
   : IRequestHandler<GetGymPaymentsQuery, List<PaymentResponse>>
    {
        public async Task<List<PaymentResponse>> Handle(GetGymPaymentsQuery request, CancellationToken cancellationToken)
        {
            var payments = await db.Payments
                .Join(db.Subscriptions,
                      payment => payment.SubscriptionId, 
                      subscription => subscription.Id,
                      (payment, subscription) => new { payment, subscription })
                .Where(ps => ps.subscription.GymId == request.id)
                .Select(ps => ps.payment)
                .ToListAsync(cancellationToken);

            var result = payments.Select(payment => new PaymentResponse(payment.Id, payment.SubscriptionId, payment.Status.ToString(), payment.Type.ToString(), payment.Amount, payment.PaymentDate)).ToList();

            return result;
        }
    }
}
