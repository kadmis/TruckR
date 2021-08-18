using System.Threading;
using System.Threading.Tasks;
using Transport.Domain.Assignments;
using Transport.Domain.Groups;
using Transport.Infrastructure.Persistence.Context;
using Transport.Infrastructure.Persistence.Repositories;

namespace Transport.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private IAssignmentsRepository _assignmentsRepository;
        private ITransportGroupsRepository _transportGroupsRepository;
        private readonly TransportContext _context;

        public UnitOfWork(TransportContext context)
        {
            _context = context;
        }

        public IAssignmentsRepository AssignmentsRepository => _assignmentsRepository ??= new AssignmentsRepository(_context);
        public ITransportGroupsRepository TransportGroupsRepository => _transportGroupsRepository ??= new TransportGroupsRepository(_context);

        public Task<int> Save(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
