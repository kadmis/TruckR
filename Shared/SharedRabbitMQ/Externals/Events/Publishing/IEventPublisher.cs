using SharedRabbitMQ.Externals.Events;
using System.Threading;
using System.Threading.Tasks;

namespace SharedRabbitMQ.Publishing
{
    public interface IEventPublisher<EventType> where EventType : IEvent
    {
        public Task Publish(EventType @event, CancellationToken cancellationToken = default);
    }
}
