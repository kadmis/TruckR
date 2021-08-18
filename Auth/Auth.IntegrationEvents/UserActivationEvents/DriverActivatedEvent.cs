using BuildingBlocks.EventBus.EventualConsistency;
using System;

namespace Auth.IntegrationEvents
{
    public class DriverActivatedEvent : IntegrationEvent
    {
        public Guid UserId { get; }

        public DriverActivatedEvent(Guid userId)
        {
            UserId = userId;
        }
    }
}
