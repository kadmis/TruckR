using System.Threading;
using System.Threading.Tasks;
using Transport.Domain.Assignments;
using Transport.Domain.Groups;

namespace Transport.Infrastructure.Persistence
{
    public interface IUnitOfWork
    {
        public IAssignmentsRepository AssignmentsRepository { get; }
        public ITransportGroupsRepository TransportGroupsRepository { get; }
        public Task<int> Save(CancellationToken cancellationToken = default);
    }
}
