using Auth.IntegrationEvents;
using BuildingBlocks.EventBus.Externals.Events.Handling;
using BuildingBlocks.EventBus.Externals.EventualConsistency.Processing.EventProcessor;
using System.Threading;
using System.Threading.Tasks;

namespace Email.API.Infrastructure.Events.UserDeleted
{
    public class UserDeletedEventHandler : IEventHandler<UserDeletedEvent>
    {
        private readonly IEventProcessor _processor;

        public UserDeletedEventHandler(IEventProcessor processor)
        {
            _processor = processor;
        }

        public async Task Handle(UserDeletedEvent @event, CancellationToken cancellationToken = default)
        {
            await _processor.SaveEvent(@event, cancellationToken);
        }
    }
}
