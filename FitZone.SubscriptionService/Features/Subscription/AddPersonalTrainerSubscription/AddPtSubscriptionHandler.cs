using FitZone.SubscriptionService.Shared.Data;
using FitZone.SubscriptionService.Shared.Domain.Entities;
using FitZone.SubscriptionService.Shared.Domain.Enums;
using MediatR;

namespace FitZone.SubscriptionService.Features.Subscription.AddSubscription
{

    public class AddPtSubscriptionHandler(AppDbContext db) : IRequestHandler<AddPtSubscriptionCommand, Guid>
    {
        public async Task<Guid> Handle(AddPtSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var subscription = new Shared.Domain.Entities.Subscription();

            subscription.ClientId = request.clientId;
            subscription.GymId = request.gymId;
            subscription.Type = SubscriptionType.PersonalTrainer;
            subscription.ClientType = ClientType.Client;
            subscription.Status = Status.Active;
            subscription.StartingDate = request.startingDate;
            subscription.EndDate = request.startingDate.AddMonths(1);

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
            var personalTrainerSub = new PersonalTrainerSubscription();
            personalTrainerSub.SubscriptionID = subscription.Id;
            personalTrainerSub.PersonalTrainerID = (Guid)request.ptId;
            subscription.PersonalTrainerSubscription = personalTrainerSub;


            var dbSubscription = db.Subscriptions.Add(subscription);
            await db.SaveChangesAsync(cancellationToken);

            return dbSubscription.Entity.Id;
        }
    }
}
