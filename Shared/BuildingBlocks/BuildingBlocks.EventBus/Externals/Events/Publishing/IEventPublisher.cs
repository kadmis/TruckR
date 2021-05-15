using System.Threading;
using System.Threading.Tasks;

namespace BuildingBlocks.EventBus.Externals.Events.Publishing
{
    public interface IEventPublisher<EventType> where EventType : IEvent
    {
        public Task Publish(EventType @event, CancellationToken cancellationToken = default);
    }
}
