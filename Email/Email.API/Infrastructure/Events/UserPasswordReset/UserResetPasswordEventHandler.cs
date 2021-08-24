using Auth.IntegrationEvents;
using BuildingBlocks.EventBus.Externals.Events.Handling;
using BuildingBlocks.EventBus.Externals.EventualConsistency.Processing.EventProcessor;
using System.Threading;
using System.Threading.Tasks;

namespace Email.API.Infrastructure.Events.UserPasswordReset
{
    public class UserResetPasswordEventHandler : IEventHandler<UserResetPasswordEvent>
    {
        private readonly IEventProcessor _processor;

        public UserResetPasswordEventHandler(IEventProcessor processor)
        {
            _processor = processor;
        }

        public async Task Handle(UserResetPasswordEvent @event, CancellationToken cancellationToken = default)
        {
            await _processor.SaveEvent(@event, cancellationToken);
        }
    }
}
