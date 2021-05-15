using BuildingBlocks.EventBus.Externals.Events;
using System;

namespace Auth.IntegrationEvents
{
    public abstract class IntegrationEvent : IEvent
    {
        public DateTime OccuredOn { get; }

        public IntegrationEvent()
        {
            OccuredOn = DateTime.Now;
        }
    }
}