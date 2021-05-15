using Auth.IntegrationEvents;
using BuildingBlocks.EventBus.Externals.Events;
using BuildingBlocks.EventBus.Externals.Events.Publishing;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Messaging
{
    public class DriverActivatedEventPublisher : IEventPublisher<DriverActivatedEvent>
    {
        private readonly IEventBusClient _client;

        public DriverActivatedEventPublisher(IEventBusClient client)
        {
            _client = client;
        }

        public Task Publish(DriverActivatedEvent @event, CancellationToken cancellationToken = default)
        {
            return _client.Publish(@event, cancellationToken);
        }
    }
}
