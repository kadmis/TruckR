using BuildingBlocks.Domain;
using System;

namespace Transport.Domain.Assignments.Events
{
    public class AssignmentCreatedDomainEvent : IDomainEvent
    {
        public AssignmentCreatedDomainEvent(Guid id, Guid creatorId)
        {
            AssignmentId = id;
            CreatorId = creatorId;
            OccurredOn = DateTime.Now;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }
        public Guid AssignmentId { get; }
        public Guid CreatorId { get; }
        public DateTime OccurredOn { get; }
    }
}
