using Auth.IntegrationEvents;
using BuildingBlocks.Application.Commands;
using BuildingBlocks.EventBus.Externals.Events;
using BuildingBlocks.EventBus.Externals.EventualConsistency.Processing.EventMapper;
using Email.API.Infrastructure.InternalCommands.SendUsernameReminderEmail;

namespace Email.API.Infrastructure.Events.UsernameReminder
{
    public class UsernameReminderEventMap : IEventToCommandMap
    {
        public ICommand Map(IEvent @event)
        {
            return new SendUsernameReminderEmailCommand(@event as UsernameReminderRequestedEvent);
        }
    }
}
