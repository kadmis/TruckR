using System.Threading;
using System.Threading.Tasks;
using TaskManagement.Infrastructure.Configuration.Processing;
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
        private readonly IDomainEventsDispatcher _dispatcher;

        public UnitOfWork(TransportContext context, IDomainEventsDispatcher dispatcher)
        {
            _context = context;
            _dispatcher = dispatcher;
        }

        public IAssignmentsRepository AssignmentsRepository => _assignmentsRepository ??= new AssignmentsRepository(_context);
        public ITransportGroupsRepository TransportGroupsRepository => _transportGroupsRepository ??= new TransportGroupsRepository(_context);

        public async Task<int> Save(CancellationToken cancellationToken = default)
        {
            await _dispatcher.DispatchEventsAsync(cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
