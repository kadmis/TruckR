using BuildingBlocks.EventBus.EventualConsistency.Database;
using BuildingBlocks.EventBus.EventualConsistency.Processing.EventProcessor;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BuildingBlocks.EventBus.EventualConsistency.Processing
{
    public class MainEventProcessor : IMainEventProcessor
    {
        private readonly IEventProcessor _scopedEventProcessor;
        private readonly IEventStore _eventService;

        public MainEventProcessor(IEventProcessor scopedEventProcessor, IEventStore eventService)
        {
            _scopedEventProcessor = scopedEventProcessor;
            _eventService = eventService;
        }

        public async Task Process(CancellationToken cancellationToken = default)
        {
            var processed = new List<EventEntity>();
            var unprocessed = await _eventService.Unprocessed(cancellationToken);

            foreach(var @event in unprocessed)
            {
                var deserialized = @event.Deserialize();

                if (await _scopedEventProcessor.ProcessEvent(deserialized, cancellationToken))
                    processed.Add(@event);
            }

            await _eventService.SetAsProcessed(processed, cancellationToken);
        }
    }
}
