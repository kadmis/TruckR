using BuildingBlocks.Domain;
using System;

namespace Transport.Domain.Assignments.Events
{
    public class AssignmentFinishedDomainEvent : IDomainEvent
    {
        public AssignmentFinishedDomainEvent(Guid id, Guid driverId)
        {
            AssignmentId = id;
            DriverId = driverId;
            OccurredOn = DateTime.Now;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }
        public Guid AssignmentId { get; }
        public Guid DriverId { get; }
        public DateTime OccurredOn { get; }
    }
}
