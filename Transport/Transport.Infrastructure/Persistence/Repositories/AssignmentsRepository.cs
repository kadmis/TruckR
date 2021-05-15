using System;
using System.Threading;
using System.Threading.Tasks;
using Transport.Domain.Assignments;
using Transport.Infrastructure.Persistence.Context;

namespace Transport.Infrastructure.Persistence.Repositories
{
    public class AssignmentsRepository : IAssignmentsRepository
    {
        private readonly TransportContext _context;

        public AssignmentsRepository(TransportContext context)
        {
            _context = context;
        }

        public async Task<Assignment> Find(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Assignments.FindAsync(new object[] { id }, cancellationToken);
        }

        Assignment IAssignmentsRepository.Add(Assignment assignment)
        {
            return _context
                .Assignments
                .Add(assignment)
                .Entity;
        }
    }
}
