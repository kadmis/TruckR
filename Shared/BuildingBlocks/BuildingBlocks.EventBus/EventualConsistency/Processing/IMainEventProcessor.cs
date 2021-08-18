using System.Threading;
using System.Threading.Tasks;

namespace BuildingBlocks.EventBus.EventualConsistency.Processing
{
    public interface IMainEventProcessor
    {
        Task Process(CancellationToken cancellationToken = default);
    }
}