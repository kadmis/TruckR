using Auth.IntegrationEvents;
using BuildingBlocks.Application.Commands;
using BuildingBlocks.EventBus.Externals.Events;
using BuildingBlocks.EventBus.Externals.EventualConsistency.Processing.EventMapper;
using Email.API.Infrastructure.InternalCommands.SendActivationEmail;

namespace Email.API.Infrastructure.Events.UserRegistered
{
    public class UserRegisteredEventMap : IEventToCommandMap
    {
        public ICommand Map(IEvent @event)
        {
            return new SendActivationEmailCommand(@event as UserRegisteredEvent);
        }
    }
}
