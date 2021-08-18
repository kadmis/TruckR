using Auth.IntegrationEvents;
using BuildingBlocks.Application.Commands;
using BuildingBlocks.EventBus.EventualConsistency.Processing.EventMapper;
using BuildingBlocks.EventBus.Externals.Events;
using Email.API.Infrastructure.InternalCommands.SendUsernameReminderEmail;

namespace Email.API.Infrastructure.Mapping
{
    public class UsernameReminderEventMap : IEventToCommandMap
    {
        public ICommand Map(IEvent @event)
        {
            return new SendUsernameReminderEmailCommand(@event as UsernameReminderRequestedEvent);
        }
    }
}
