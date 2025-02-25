using FitZone.SubscriptionService.Features.Payment.GetPayments;
using MediatR;

namespace FitZone.SubscriptionService.Features.Payment.GetGymPayments
{
    public record GetGymPaymentsQuery(Guid id) : IRequest<List<PaymentResponse>>;
}
