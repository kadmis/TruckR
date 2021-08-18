using BuildingBlocks.EventBus.Externals.Events;
using System.Threading;
using System.Threading.Tasks;

namespace BuildingBlocks.EventBus.EventualConsistency.Processing.EventProcessor
{
    public interface IEventProcessor
    {
        Task<bool> SaveEvent<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IEvent;
        Task<bool> ProcessEvent<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IEvent;
    }
}