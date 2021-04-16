using Location.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Location.Domain.Repositories
{
    public interface IStatisticsReadOnlyRepository
    {
        Task<IEnumerable<Statistic>> GetForUser(Guid userId, CancellationToken cancellationToken = default);
    }
}
