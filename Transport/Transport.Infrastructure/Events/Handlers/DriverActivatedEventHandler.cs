﻿using Auth.IntegrationEvents;
using BuildingBlocks.EventBus.Externals.Events.Handling;
using BuildingBlocks.EventBus.Externals.EventualConsistency.Processing.EventProcessor;
using System.Threading;
using System.Threading.Tasks;

namespace Transport.Infrastructure.Events.Handlers
{
    public class DriverActivatedEventHandler : IEventHandler<DriverActivatedEvent>
    {
        private readonly IEventProcessor _processor;

        public DriverActivatedEventHandler(IEventProcessor processor)
        {
            _processor = processor;
        }

        public async Task Handle(
            DriverActivatedEvent @event, 
            CancellationToken cancellationToken = default)
        {
            await _processor.SaveEvent(
                @event, cancellationToken);
        }
    }
}
