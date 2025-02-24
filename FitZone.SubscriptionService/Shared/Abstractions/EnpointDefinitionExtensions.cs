using System.Reflection;

namespace FitZone.SubscriptionService.Shared.Abstractions
{
    public static class EndpointDefinitionExtensions
    {
        public static void RegisterAllEndpoints(this IEndpointRouteBuilder app)
        {
            var endpointTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && typeof(IEndpointDefinition).IsAssignableFrom(t));

            foreach (var type in endpointTypes)
            {
                var endpointInstance = Activator.CreateInstance(type) as IEndpointDefinition;
                endpointInstance?.RegisterEndpoints(app);
            }
        }
    }
}
