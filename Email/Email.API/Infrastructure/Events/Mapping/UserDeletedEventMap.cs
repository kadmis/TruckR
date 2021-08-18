using Auth.IntegrationEvents;
using BuildingBlocks.Application.Commands;
using BuildingBlocks.EventBus.EventualConsistency.Processing.EventMapper;
using BuildingBlocks.EventBus.Externals.Events;
using Email.API.Infrastructure.InternalCommands.SendUserDeletionEmail;

namespace Email.API.Infrastructure.Mapping
{
    public class UserDeletedEventMap : IEventToCommandMap
    {
        public ICommand Map(IEvent @event)
        {
            return new SendUserDeletionEmailCommand(@event as UserDeletedEvent);
        }
    }
}
