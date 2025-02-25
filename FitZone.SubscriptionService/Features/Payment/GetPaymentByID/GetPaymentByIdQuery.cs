using FitZone.SubscriptionService.Features.Payment.GetPayments;
using MediatR;

namespace FitZone.SubscriptionService.Features.Payment.GetPaymentByID
{
    public record GetPaymentByIdQuery(Guid id) : IRequest<PaymentResponse>;
}
