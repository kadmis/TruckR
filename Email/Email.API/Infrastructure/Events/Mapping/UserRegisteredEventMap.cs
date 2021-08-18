using Auth.IntegrationEvents;
using BuildingBlocks.Application.Commands;
using BuildingBlocks.EventBus.EventualConsistency.Processing.EventMapper;
using BuildingBlocks.EventBus.Externals.Events;
using Email.API.Infrastructure.InternalCommands.SendActivationEmail;

namespace Email.API.Infrastructure.Mapping
{
    public class UserRegisteredEventMap : IEventToCommandMap
    {
        public ICommand Map(IEvent @event)
        {
            return new SendActivationEmailCommand(@event as UserRegisteredEvent);
        }
    }
}
