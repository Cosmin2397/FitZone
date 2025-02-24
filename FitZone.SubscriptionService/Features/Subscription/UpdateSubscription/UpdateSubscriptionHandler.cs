using FitZone.SubscriptionService.Features.Subscription.AddSubscription;
using FitZone.SubscriptionService.Shared.Data;
using FitZone.SubscriptionService.Shared.Domain.Entities;
using FitZone.SubscriptionService.Shared.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;

namespace FitZone.SubscriptionService.Features.Subscription.UpdateSubscription
{

    public class UpdateSubscriptionHandler(AppDbContext db) : IRequestHandler<UpdateSubscriptionCommand, bool>
    {
        public async Task<bool> Handle(UpdateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var subscription = await db.Subscriptions.Where(i => i.Id == request.subId).FirstOrDefaultAsync(cancellationToken);
            if (subscription != null)
            {
                if (request.status == "Canceled")
                {
                    subscription.Status = Enum.Parse<Status>(request.status);
                    subscription.EndDate = DateTime.Now;
                }
                else if (request.subscriptionType == "PersonalTrainer")
                {
                    subscription.Type = Enum.Parse<SubscriptionType>(request.subscriptionType);
                    var personalTrainerSub = new PersonalTrainerSubscription();
                    personalTrainerSub.SubscriptionID = subscription.Id;
                    personalTrainerSub.PersonalTrainerID = (Guid)request.personalTrainerID;
                    subscription.PersonalTrainerSubscription = personalTrainerSub;
                }
                else if (request.addedDays > 0)
                {
                    subscription.EndDate.AddDays(request.addedDays);
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
                var result = await db.SaveChangesAsync(cancellationToken);

                return result > 0;
            }

            return false;
        }
    }
}
