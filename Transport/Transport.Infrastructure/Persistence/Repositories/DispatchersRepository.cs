using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Transport.Domain.Dispatchers;
using Transport.Infrastructure.Persistence.Context;

namespace Transport.Infrastructure.Persistence.Repositories
{
    public class DispatchersRepository : IDispatchersRepository
    {
        private readonly TransportContext _context;

        public DispatchersRepository(TransportContext context)
        {
            _context = context;
        }

        public Dispatcher Add(Dispatcher dispatcher)
        {
            return _context.Dispatchers
                .Add(dispatcher)
                .Entity;
        }

        public async Task<Dispatcher> FindById(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Dispatchers.FindAsync(new object[] { id }, cancellationToken);
        }
    }
}
