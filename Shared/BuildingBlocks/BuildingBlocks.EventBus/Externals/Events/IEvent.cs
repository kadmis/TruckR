using System;

namespace BuildingBlocks.EventBus.Externals.Events
{
    public interface IEvent
    {
        public Guid Id { get; }
        public DateTime OccuredOn { get; }
    }
}
