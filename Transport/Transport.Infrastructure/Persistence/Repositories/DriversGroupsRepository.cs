using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transport.Domain.DriverGroups;
using Transport.Infrastructure.Persistence.Context;

namespace Transport.Infrastructure.Persistence.Repositories
{
    public class DriversGroupsRepository : IDriversGroupsRepository
    {
        private readonly TransportContext _context;

        public DriversGroupsRepository(TransportContext context)
        {
            _context = context;
        }

        public DriversGroup Add(DriversGroup driversGroup)
        {
            throw new NotImplementedException();
        }
    }
}
