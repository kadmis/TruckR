using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Transport.Domain.Drivers;
using Transport.Infrastructure.Persistence.Context;

namespace Transport.Infrastructure.Persistence.Repositories
{
    public class DriversRepository : IDriversRepository
    {
        private readonly TransportContext _context;

        public DriversRepository(TransportContext context)
        {
            _context = context;
        }

        public Driver Add(Driver driver)
        {
            return _context.Drivers
                .Add(driver)
                .Entity;
        }

        public async Task<Driver> FindById(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Drivers.FindAsync(new object[] { id }, cancellationToken);
        }
    }
}
