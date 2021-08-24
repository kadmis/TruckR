using System.Threading;
using System.Threading.Tasks;

namespace TaskManagement.Infrastructure.Configuration.Processing
{
    public interface IDomainEventsDispatcher
    {
        Task DispatchEventsAsync(CancellationToken cancellationToken = default);
    }
}
