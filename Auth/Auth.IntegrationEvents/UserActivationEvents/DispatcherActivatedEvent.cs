using BuildingBlocks.EventBus.EventualConsistency;
using System;

namespace Auth.IntegrationEvents
{
    public class DispatcherActivatedEvent : IntegrationEvent
    {
        public Guid UserId { get; }

        public DispatcherActivatedEvent(Guid userId):base()
        {
            UserId = userId;
        }
    }
}
