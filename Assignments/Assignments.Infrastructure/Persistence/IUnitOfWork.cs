using System.Threading;
using System.Threading.Tasks;
using Transport.Domain.Assignments;
using Transport.Domain.Employees;

namespace Transport.Infrastructure.Persistence
{
    public interface IUnitOfWork
    {
        public IAssignmentsRepository AssignmentsRepository { get; }
        public IEmployeesRepository ActorsRepository { get; }
        public Task<int> Save(CancellationToken cancellationToken = default);
    }
}
