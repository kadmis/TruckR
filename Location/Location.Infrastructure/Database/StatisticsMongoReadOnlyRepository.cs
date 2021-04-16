using Location.Domain.Entities;
using Location.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Location.Infrastructure.Database
{
    internal class StatisticsMongoReadOnlyRepository : IStatisticsReadOnlyRepository
    {
        private readonly LocationMongoContext _context;

        public StatisticsMongoReadOnlyRepository(LocationMongoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Statistic>> GetForUser(Guid userId, CancellationToken cancellationToken = default)
        {
            return await AsQueryable()
                .Where(x => x.UserId == userId)
                .ToListAsync(cancellationToken: cancellationToken);
        }

        private IMongoQueryable<Statistic> AsQueryable()
        {
            return _context.Statistics.AsQueryable();
        }
    }
}