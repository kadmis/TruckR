using BuildingBlocks.EventBus.Externals.Events;
using System;

namespace BuildingBlocks.EventBus.EventualConsistency
{
    public abstract class IntegrationEvent : IEvent
    {
        public Guid Id { get; }
        public DateTime OccuredOn { get; }

        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            OccuredOn = DateTime.Now;
        }
    }
}