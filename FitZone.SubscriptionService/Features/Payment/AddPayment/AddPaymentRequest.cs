namespace FitZone.SubscriptionService.Features.Payment.AddPayment
{
    public sealed record AddPaymentRequest(Guid subscriptionId, string status, string type, decimal amount, DateTime paymentDate);
}
