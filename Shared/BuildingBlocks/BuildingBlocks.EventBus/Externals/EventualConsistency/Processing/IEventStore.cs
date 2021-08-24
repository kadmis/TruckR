using BuildingBlocks.EventBus.Externals.Events;
using BuildingBlocks.EventBus.Externals.EventualConsistency.Database;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BuildingBlocks.EventBus.Externals.EventualConsistency.Processing
{
    public interface IEventStore
    {
        public Task Add<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IEvent;
        public Task SetAsProcessed(IEnumerable<EventEntity> events, CancellationToken cancellationToken = default);
        public Task<IEnumerable<EventEntity>> Unprocessed(CancellationToken cancellationToken = default);
    }
}
