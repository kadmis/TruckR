using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Transport.Domain.Drivers
{
    public interface IDriversRepository
    {
        Task<Driver> FindById(Guid id, CancellationToken cancellationToken = default);
        Driver Add(Driver driver);
    }
}
