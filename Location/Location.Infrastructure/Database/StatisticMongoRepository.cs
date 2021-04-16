using Location.Domain.Entities;
using Location.Domain.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Location.Infrastructure.Database
{
    internal class StatisticMongoRepository : IStatisticsRepository
    {
        private readonly LocationMongoContext _context;

        public StatisticMongoRepository(LocationMongoContext context)
        {
            _context = context;
        }

        public Task Add(Statistic statistic, CancellationToken cancellationToken = default)
        {
            return _context.Statistics.InsertOneAsync(statistic, cancellationToken:cancellationToken);
        }
        public Task Add(IEnumerable<Statistic> statistics, CancellationToken cancellationToken = default)
        {
            return _context.Statistics.InsertManyAsync(statistics, cancellationToken:cancellationToken);
        }
    }
}
