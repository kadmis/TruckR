using Auth.IntegrationEvents;
using BuildingBlocks.Application.Commands;
using BuildingBlocks.EventBus.Externals.Events;
using BuildingBlocks.EventBus.Externals.EventualConsistency.Processing.EventMapper;
using Transport.Infrastructure.InternalCommands.CreateGroup;

namespace Transport.Infrastructure.Events.Mapping
{
    public class DispatcherActivatedEventMap : IEventToCommandMap
    {
        public ICommand Map(IEvent @event)
        {
            return new CreateGroupCommand(@event as DispatcherActivatedEvent);
        }
    }
}
