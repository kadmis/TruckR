using BuildingBlocks.Application.Commands;
using BuildingBlocks.EventBus.Externals.Events;

namespace BuildingBlocks.EventBus.Externals.EventualConsistency.Processing.EventMapper
{
    public interface IEventToCommandMap
    {
        public ICommand Map(IEvent @event);
    }
}
