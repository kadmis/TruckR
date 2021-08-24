using BuildingBlocks.EventBus.Externals.EventualConsistency.Processing;
using Quartz;
using System.Threading.Tasks;

namespace Transport.Infrastructure.Events.Processing
{
    [DisallowConcurrentExecution]
    public class EventProcessingJob : IJob
    {
        private readonly IMainEventProcessor _eventProcessor;

        public EventProcessingJob(IMainEventProcessor eventProcessor)
        {
            _eventProcessor = eventProcessor;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _eventProcessor.Process(context.CancellationToken);
        }
    }
}
