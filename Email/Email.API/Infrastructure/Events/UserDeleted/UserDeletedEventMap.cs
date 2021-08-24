using Auth.IntegrationEvents;
using BuildingBlocks.Application.Commands;
using BuildingBlocks.EventBus.Externals.Events;
using BuildingBlocks.EventBus.Externals.EventualConsistency.Processing.EventMapper;
using Email.API.Infrastructure.InternalCommands.SendUserDeletionEmail;

namespace Email.API.Infrastructure.Events.UserDeleted
{
    public class UserDeletedEventMap : IEventToCommandMap
    {
        public ICommand Map(IEvent @event)
        {
            return new SendUserDeletionEmailCommand(@event as UserDeletedEvent);
        }
    }
}
