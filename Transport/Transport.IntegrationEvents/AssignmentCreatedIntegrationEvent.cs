using BuildingBlocks.EventBus.EventualConsistency;
using BuildingBlocks.EventBus.Externals.Events;
using System;
using Transport.Domain.Assignments.Events;

namespace Transport.IntegrationEvents
{
    public class AssignmentCreatedIntegrationEvent : IntegrationEvent
    {
        private AssignmentCreatedIntegrationEvent()
        {
        }

        public static AssignmentCreatedIntegrationEvent From(AssignmentCreatedDomainEvent @event)
        {
            return new AssignmentCreatedIntegrationEvent
            {
                AssignmentId = @event.AssignmentId,
                CreatorId = @event.CreatorId,
                OccuredOn = @event.OccurredOn
            };
        }
        public Guid AssignmentId { get; private set; }
        public Guid CreatorId { get; private set; }
        public DateTime OccuredOn { get; private set; }
    }
}
