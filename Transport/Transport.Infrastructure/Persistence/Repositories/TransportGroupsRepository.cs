using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Transport.Domain.Groups;
using Transport.Infrastructure.Persistence.Context;

namespace Transport.Infrastructure.Persistence.Repositories
{
    public class TransportGroupsRepository : ITransportGroupsRepository
    {
        private readonly TransportContext _context;

        public TransportGroupsRepository(TransportContext context)
        {
            _context = context;
        }

        public TransportGroup Add(TransportGroup transportGroup)
        {
            return _context
                .TransportGroups
                .Add(transportGroup)
                .Entity;
        }

        public TransportGroup Update(TransportGroup transportGroup)
        {
            return _context
                .TransportGroups
                .Update(transportGroup)
                .Entity;
        }

        public async Task<TransportGroup> FindById(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.TransportGroups.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<TransportGroup> FindGroupWithFreeSpots(CancellationToken cancellationToken = default)
        {
            return await _context.TransportGroups.FirstOrDefaultAsync(x => x.Drivers.Count() < TransportGroup.DriverLimit, cancellationToken);
        }
    }
}
