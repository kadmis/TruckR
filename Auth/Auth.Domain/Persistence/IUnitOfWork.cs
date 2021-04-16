using Auth.Domain.Persistence.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Domain.Persistence
{
    public interface IUnitOfWork
    {
        Task<int> Save(CancellationToken cancellationToken = default);
        IUserRepository UserRepository { get; }
    }
}
