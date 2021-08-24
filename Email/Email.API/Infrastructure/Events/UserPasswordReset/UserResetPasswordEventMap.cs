using Auth.IntegrationEvents;
using BuildingBlocks.Application.Commands;
using BuildingBlocks.EventBus.Externals.Events;
using BuildingBlocks.EventBus.Externals.EventualConsistency.Processing.EventMapper;
using Email.API.Infrastructure.InternalCommands.SendPasswordResetEmail;

namespace Email.API.Infrastructure.Events.UserPasswordReset
{
    public class UserResetPasswordEventMap : IEventToCommandMap
    {
        public ICommand Map(IEvent @event)
        {
            return new SendPasswordResetEmailCommand(@event as UserResetPasswordEvent);
        }
    }
}
