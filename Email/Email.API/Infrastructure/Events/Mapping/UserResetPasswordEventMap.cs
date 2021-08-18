using Auth.IntegrationEvents;
using BuildingBlocks.Application.Commands;
using BuildingBlocks.EventBus.EventualConsistency.Processing.EventMapper;
using BuildingBlocks.EventBus.Externals.Events;
using Email.API.Infrastructure.InternalCommands.SendPasswordResetEmail;

namespace Email.API.Infrastructure.Mapping
{
    public class UserResetPasswordEventMap : IEventToCommandMap
    {
        public ICommand Map(IEvent @event)
        {
            return new SendPasswordResetEmailCommand(@event as UserResetPasswordEvent);
        }
    }
}
