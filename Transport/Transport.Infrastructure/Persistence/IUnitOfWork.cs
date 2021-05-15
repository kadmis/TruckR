using System.Threading;
using System.Threading.Tasks;

namespace Transport.Infrastructure.Persistence
{
    public interface IUnitOfWork
    {
        public Task<int> Save(CancellationToken cancellationToken = default);
    }
}
