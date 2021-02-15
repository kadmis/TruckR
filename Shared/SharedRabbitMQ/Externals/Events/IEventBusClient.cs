using System;
using System.Threading;
using System.Threading.Tasks;

namespace SharedRabbitMQ.Externals.Events
{
    public interface IEventBusClient
    {
        public Task Publish<EventType>(EventType @event, CancellationToken cancellationToken = default) where EventType : IEvent;
        public Task Subscribe<EventType>(Func<EventType, Task> subscribeMethod, CancellationToken cancellationToken = default) where EventType : IEvent;
    }
}
