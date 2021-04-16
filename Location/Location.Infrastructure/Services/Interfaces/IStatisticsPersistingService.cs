using System.Threading;
using System.Threading.Tasks;

namespace Location.Infrastructure.Services.Interfaces
{
    public interface IStatisticsPersistingService
    {
        Task Persist(CancellationToken cancellationToken = default);
    }
}
