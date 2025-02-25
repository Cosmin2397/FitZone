using FitZone.SubscriptionService.Features.Payment.GetGymPayments;
using FitZone.SubscriptionService.Features.Payment.GetPayments;
using FitZone.SubscriptionService.Shared.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitZone.SubscriptionService.Features.Payment.GetPaymentByID
{
    public class GetPaymentByIdHandler(AppDbContext db)
: IRequestHandler<GetPaymentByIdQuery, PaymentResponse>
    {
        public async Task<PaymentResponse> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
        {
            var payment = await db.Payments
                .Where(ps => ps.Id == request.id)
                .FirstOrDefaultAsync(cancellationToken);

            var result =  new PaymentResponse(payment.Id, payment.SubscriptionId, payment.Status.ToString(), payment.Type.ToString(), payment.Amount, payment.PaymentDate);

            return result;
        }
    }
}
