﻿using Auth.IntegrationEvents;
using BuildingBlocks.EventBus.EventualConsistency.Processing.EventProcessor;
using BuildingBlocks.EventBus.Externals.Events.Handling;
using System.Threading;
using System.Threading.Tasks;

namespace Transport.Infrastructure.Events.Handlers
{
    public class DispatcherActivatedEventHandler : IEventHandler<DispatcherActivatedEvent>
    {
        private readonly IEventProcessor _processor;

        public DispatcherActivatedEventHandler(IEventProcessor processor)
        {
            _processor = processor;
        }

        public async Task Handle(DispatcherActivatedEvent @event, CancellationToken cancellationToken = default)
        {
            await _processor.SaveEvent(@event, cancellationToken);
        }
    }
}
