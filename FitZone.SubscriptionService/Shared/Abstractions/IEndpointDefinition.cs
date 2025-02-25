namespace FitZone.SubscriptionService.Shared.Abstractions
{
    public interface IEndpointDefinition
    {
        void RegisterEndpoints(IEndpointRouteBuilder app);
    }
}
