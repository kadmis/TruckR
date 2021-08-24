using System.Threading;
using System.Threading.Tasks;

namespace BuildingBlocks.EventBus.Externals.EventualConsistency.Processing
{
    public interface IMainEventProcessor
    {
        Task Process(CancellationToken cancellationToken = default);
    }
}