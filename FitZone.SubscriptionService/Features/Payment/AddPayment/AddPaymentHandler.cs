using FitZone.SubscriptionService.Features.Subscription.AddSubscription;
using FitZone.SubscriptionService.Shared.Data;
using FitZone.SubscriptionService.Shared.Domain.Enums;
using MediatR;

namespace FitZone.SubscriptionService.Features.Payment.AddPayment
{

    public class AddPaymentHandler(AppDbContext db) : IRequestHandler<AddPaymentCommand, Guid>
    {
        public async Task<Guid> Handle(AddPaymentCommand request, CancellationToken cancellationToken)
        {
                var payment = new Shared.Domain.Entities.Payment();
                payment.SubscriptionId = request.subscriptionId;
                payment.Status = Enum.Parse<PaymentStatus>(request.status);
                payment.Type = Enum.Parse<PaymentType>(request.type);
                payment.Amount = request.amount;
                payment.PaymentDate = request.paymentDate;


            var dbPayment = db.Payments.Add(payment);
            await db.SaveChangesAsync(cancellationToken);

            return dbPayment.Entity.Id;
        }
    }
}
