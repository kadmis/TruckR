using Auth.IntegrationEvents;
using BuildingBlocks.EventBus.EventualConsistency.Processing.EventProcessor;
using BuildingBlocks.EventBus.Externals.Events.Handling;
using System.Threading;
using System.Threading.Tasks;

namespace Email.API.Infrastructure.Events.Handlers
{
    public class UsernameReminderRequestedEventHandler : IEventHandler<UsernameReminderRequestedEvent>
    {
        private readonly IEventProcessor _processor;

        public UsernameReminderRequestedEventHandler(IEventProcessor processor)
        {
            _processor = processor;
        }

        public async Task Handle(UsernameReminderRequestedEvent @event, CancellationToken cancellationToken = default)
        {
            await _processor.SaveEvent(@event, cancellationToken);
        }
    }
}
