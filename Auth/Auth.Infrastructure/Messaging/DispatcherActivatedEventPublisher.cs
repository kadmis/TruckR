using Auth.IntegrationEvents;
using BuildingBlocks.EventBus.Externals.Events;
using BuildingBlocks.EventBus.Externals.Events.Publishing;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Messaging
{
    public class DispatcherActivatedEventPublisher : IEventPublisher<DispatcherActivatedEvent>
    {
        private readonly IEventBusClient _client;

        public DispatcherActivatedEventPublisher(IEventBusClient client)
        {
            _client = client;
        }

        public Task Publish(DispatcherActivatedEvent @event, CancellationToken cancellationToken = default)
        {
            return _client.Publish(@event, cancellationToken);
        }
    }
}
