using Microsoft.Extensions.DependencyInjection;
using Transport.SignalRClient.Clients.Shared;

namespace Transport.SignalRClient
{
    public static class DependencyInjection
    {
        public static void AddHubClients(this IServiceCollection services, string url)
        {
            services.AddSingleton(new HubConfiguration(url));
            services.AddTransient<ITransportHubClient, TransportHubClient>();
        }
    }
}
