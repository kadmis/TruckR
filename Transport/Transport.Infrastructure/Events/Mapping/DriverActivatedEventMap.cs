using Auth.IntegrationEvents;
using BuildingBlocks.Application.Commands;
using BuildingBlocks.EventBus.EventualConsistency.Processing.EventMapper;
using BuildingBlocks.EventBus.Externals.Events;
using Transport.Infrastructure.InternalCommands.AddDriverToGroup;

namespace Transport.Infrastructure.Events.Mapping
{
    public class DriverActivatedEventMap : IEventToCommandMap
    {
        public ICommand Map(IEvent @event)
        {
            return new AddDriverToGroupCommand(@event as DriverActivatedEvent);
        }
    }
}
