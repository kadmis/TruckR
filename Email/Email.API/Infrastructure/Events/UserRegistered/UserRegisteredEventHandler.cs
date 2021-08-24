using Auth.IntegrationEvents;
using BuildingBlocks.EventBus.Externals.Events.Handling;
using BuildingBlocks.EventBus.Externals.EventualConsistency.Processing.EventProcessor;
using System.Threading;
using System.Threading.Tasks;

namespace Email.API.Infrastructure.Events.UserRegistered
{
    public class UserRegisteredEventHandler : IEventHandler<UserRegisteredEvent>
    {
        private readonly IEventProcessor _processor;

        public UserRegisteredEventHandler(IEventProcessor processor)
        {
            _processor = processor;
        }

        public async Task Handle(UserRegisteredEvent @event, CancellationToken cancellationToken = default)
        {
            await _processor.SaveEvent(@event, cancellationToken);
        }
    }
}
