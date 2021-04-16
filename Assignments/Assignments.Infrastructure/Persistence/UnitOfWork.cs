using Transport.Infrastructure.Persistence.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;
using Transport.Domain.Assignments;
using Transport.Infrastructure.Persistence.Context;
using Transport.Domain.Employees;

namespace Transport.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TransportContext _context;
        private IAssignmentsRepository _assignmentsRepository;
        private IEmployeesRepository _actorsRepository;

        public UnitOfWork(TransportContext context)
        {
            _context = context;
        }

        public IAssignmentsRepository AssignmentsRepository => _assignmentsRepository ??= new AssignmentsRepository(_context);
        public IEmployeesRepository ActorsRepository => _actorsRepository ??= new EmployeesRepository(_context);

        public Task<int> Save(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
