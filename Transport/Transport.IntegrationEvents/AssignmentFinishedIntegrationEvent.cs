using BuildingBlocks.EventBus.EventualConsistency;
using System;
using Transport.Domain.Assignments.Events;

namespace Transport.IntegrationEvents
{
    public class AssignmentFinishedIntegrationEvent : IntegrationEvent
    {
        private AssignmentFinishedIntegrationEvent()
        {
        }

        public static AssignmentFinishedIntegrationEvent From(AssignmentFinishedDomainEvent @event)
        {
            return new AssignmentFinishedIntegrationEvent
            {
                AssignmentId = @event.AssignmentId,
                DriverId = @event.DriverId,
                OccuredOn = @event.OccurredOn
            };
        }
        public Guid AssignmentId { get; private set; }
        public Guid DriverId { get; private set; }
        public DateTime OccuredOn { get; private set; }
    }
}
