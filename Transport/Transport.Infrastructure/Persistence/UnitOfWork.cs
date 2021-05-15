using System.Threading;
using System.Threading.Tasks;
using Transport.Infrastructure.Persistence.Context;

namespace Transport.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TransportContext _context;

        public UnitOfWork(TransportContext context)
        {
            _context = context;
        }

        public Task<int> Save(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
