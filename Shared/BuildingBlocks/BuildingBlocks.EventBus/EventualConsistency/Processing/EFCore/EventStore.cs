using BuildingBlocks.Domain;
using BuildingBlocks.EventBus.EventualConsistency.Database;
using BuildingBlocks.EventBus.EventualConsistency.Database.EFCore;
using BuildingBlocks.EventBus.Externals.Events;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BuildingBlocks.EventBus.EventualConsistency.Processing.EFCore
{
    public class EventStore : IEventStore
    {
        private readonly IEventContext _context;

        public EventStore(IEventContext context)
        {
            _context = context;
        }

        public async Task Add<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
            where TEvent : IEvent
        {
            var entity = EventEntity.Create(@event);
            _context.Events.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<EventEntity>> Unprocessed(CancellationToken cancellationToken = default)
        {
            return await _context.Events
                .Where(x=>!x.ProcessedDate.HasValue)
                .ToListAsync(cancellationToken);
        }

        public async Task SetAsProcessed(IEnumerable<EventEntity> events, CancellationToken cancellationToken = default)
        {
            foreach (var @event in events)
                @event.ProcessedDate = Clock.Now;

            _context.Events.UpdateRange(events);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
