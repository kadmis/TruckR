using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BuildingBlocks.EventBus.Externals.EventualConsistency.Database.EFCore
{
    public interface IEventContext
    {
        public DbSet<EventEntity> Events { get; }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
