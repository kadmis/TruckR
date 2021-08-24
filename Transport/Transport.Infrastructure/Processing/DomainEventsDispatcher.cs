using BuildingBlocks.Domain;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskManagement.Infrastructure.Configuration.Processing;
using Transport.Infrastructure.Persistence.Context;

namespace Transport.Infrastructure.Processing
{
    public class DomainEventsDispatcher : IDomainEventsDispatcher
    {
        private readonly IMediator _mediator;
        private readonly TransportContext _context;

        public DomainEventsDispatcher(IMediator mediator, TransportContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        public async Task DispatchEventsAsync(CancellationToken cancellationToken = default)
        {
            var domainEntities = _context.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
                .ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    await _mediator.Publish(domainEvent, cancellationToken);
                });

            await Task.WhenAll(tasks);
        }
    }
}
