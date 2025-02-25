using FitZone.SubscriptionService.Features.Subscription.GetSubscriptions;
using FitZone.SubscriptionService.Shared.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitZone.SubscriptionService.Features.Payment.GetPayments
{
    public class GetPaymentHandler(AppDbContext db)
    : IRequestHandler<GetPaymentsQuery, List<PaymentResponse>>
    {
        public async Task<List<PaymentResponse>> Handle(GetPaymentsQuery request, CancellationToken cancellationToken)
        {
            var payments = await db.Payments
                .ToListAsync(cancellationToken);

            var result = payments.Select(payment => new PaymentResponse(payment.Id,payment.SubscriptionId,payment.Status.ToString(),payment.Type.ToString(),payment.Amount,payment.PaymentDate)).ToList();

            return result;
        }
    }
}
