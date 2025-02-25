using FitZone.SubscriptionService.Features.Subscription.GetSubscriptions;
using MediatR;

namespace FitZone.SubscriptionService.Features.Payment.GetPayments
{
    public record GetPaymentsQuery() : IRequest<List<PaymentResponse>>;
}
