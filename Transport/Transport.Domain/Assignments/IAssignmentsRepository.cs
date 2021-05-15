using System;
using System.Threading;
using System.Threading.Tasks;

namespace Transport.Domain.Assignments
{
    public interface IAssignmentsRepository
    {
        Task<Assignment> Find(Guid id, CancellationToken cancellationToken = default);
        Assignment Add(Assignment assignment);
    }
}
