using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Transport.Domain.Dispatchers
{
    public interface IDispatchersRepository
    {
        Task<Dispatcher> FindById(Guid id, CancellationToken cancellationToken = default);
        Dispatcher Add(Dispatcher dispatcher);
    }
}
