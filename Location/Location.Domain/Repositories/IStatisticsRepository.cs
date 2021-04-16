using Location.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Location.Domain.Repositories
{
    public interface IStatisticsRepository
    {
        Task Add(Statistic statistic, CancellationToken cancellationToken = default);
        Task Add(IEnumerable<Statistic> statistics, CancellationToken cancellationToken = default);
    }
}
