using BuildingBlocks.EventBus.Externals.Events;
using System;

namespace BuildingBlocks.EventBus.Externals.EventualConsistency
{
    public abstract class IntegrationEvent : IEvent
    {
        public Guid Id { get; }
        public DateTime OccuredOn { get; protected set; }

        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            OccuredOn = DateTime.Now;
        }
    }
}