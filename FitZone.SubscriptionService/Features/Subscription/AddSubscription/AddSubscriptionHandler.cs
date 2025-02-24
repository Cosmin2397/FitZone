using FitZone.SubscriptionService.Shared.Data;
using FitZone.SubscriptionService.Shared.Domain.Enums;
using MediatR;

namespace FitZone.SubscriptionService.Features.Subscription.AddSubscription
{

    public class AddSubscriptionHandler(AppDbContext db) : IRequestHandler<AddSubscriptionCommand, Guid>
    {
        public async Task<Guid> Handle(AddSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var subscription = new Shared.Domain.Entities.Subscription();

            subscription.ClientId = request.clientId;
            subscription.GymId = request.gymId;
            subscription.Type = Enum.Parse<SubscriptionType>(request.subscriptionType);
            subscription.ClientType = Enum.Parse<ClientType>(request.clientType);
            subscription.Status = Status.Active;
            subscription.StartingDate = request.startingDate;
            if(subscription.ClientType == ClientType.Client)
            {
                subscription.EndDate = request.startingDate.AddMonths(1);
            }
            else
            {
                subscription.EndDate = DateTime.MaxValue;
            }

            if (request.payment != null)
            {
                var payment = new Shared.Domain.Entities.Payment();
                payment.Status = Enum.Parse<PaymentStatus>(request.payment.status);
                payment.Type = Enum.Parse<PaymentType>(request.payment.type);
                payment.Amount = request.payment.amount;
                payment.PaymentDate = request.payment.paymentDate;
                subscription.SubscriptionPayments = new List<Shared.Domain.Entities.Payment>();
                subscription.SubscriptionPayments.Add(payment);
            }

            var dbSubscription = db.Subscriptions.Add(subscription);
            await db.SaveChangesAsync(cancellationToken);

            return dbSubscription.Entity.Id;
        }
    }
}
