using BuildingBlocks.Application.Commands;
using BuildingBlocks.EventBus.Externals.Events;

namespace BuildingBlocks.EventBus.EventualConsistency.Processing.EventMapper
{
    public interface IEventToCommandMap
    {
        public ICommand Map(IEvent @event);
    }
}
