using RawRabbit;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SharedRabbitMQ.Externals.Events
{
    public class RabbitClient : IEventBusClient
    {
        private readonly IBusClient _busClient;

        public RabbitClient(IBusClient busClient)
        {
            _busClient = busClient ?? throw new ArgumentNullException(nameof(busClient));
        }

        public Task Publish<EventType>(EventType @event, CancellationToken cancellationToken = default) where EventType : IEvent
        {
            return _busClient.PublishAsync(@event, default, cancellationToken);
        }

        public Task Subscribe<EventType>(Func<EventType, Task> subscribeMethod, CancellationToken cancellationToken = default) where EventType : IEvent
        {
            return _busClient.SubscribeAsync(subscribeMethod, default, cancellationToken);
        }
    }
}
