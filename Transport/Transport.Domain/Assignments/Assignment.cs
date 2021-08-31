using BuildingBlocks.Domain;
using System;
using Transport.Domain.Documents;
using Transport.Domain.Assignments.Events;
using Transport.Domain.Groups;
using Transport.Domain.Assignments.Rules;

namespace Transport.Domain.Assignments
{
    public class Assignment : Entity, IAggregateRoot
    {
        //Immutables
        public Guid Id { get; }

        private readonly string _title;

        private readonly string _description;

        private readonly Document _transportDocument;

        private readonly Guid  _dispatcherId;

        private readonly Address _start;

        private readonly Address _destination;

        private readonly DateTime _deadline;

        private readonly DateTime _createdOn;

        //Mutables
        private Guid? _driverId;

        private DateTime? _assignedOn;

        private DateTime? _completedOn;

        public bool Assigned => _assignedOn.HasValue && _driverId.HasValue;
        public bool Completed => _completedOn.HasValue;

        public Guid DocumentId => _transportDocument.Id;

        private Assignment()
        {
        }
        private Assignment(
            string title, 
            string description, 
            Document transportDocument, 
            Guid dispatcherId, 
            DateTime deadline,
            Address start,
            Address destination)
        {
            Id = Guid.NewGuid();
            _title = title;
            _description = description;
            _transportDocument = transportDocument;
            _dispatcherId = dispatcherId;
            _deadline = deadline;
            _start = start;
            _destination = destination;
            _createdOn = Clock.Now;
        }

        /// <summary>
        /// Creates a new assignment.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="transportDocument"></param>
        /// <param name="dispatcher"></param>
        /// <param name="deadline"></param>
        /// <returns></returns>
        public static Assignment Create(
            string title, 
            string description, 
            Document transportDocument, 
            Guid dispatcherId, 
            DateTime deadline,
            Address start,
            Address destination)
        {
            var assignment = new Assignment(
                title,
                description,
                transportDocument,
                dispatcherId,
                deadline,
                start,
                destination);

            assignment.AddDomainEvent(new AssignmentCreatedDomainEvent(assignment.Id, assignment._dispatcherId));

            return assignment;
        }

        /// <summary>
        /// Assigns a driver to this assignment.
        /// </summary>
        /// <param name="driver">Given driver.</param>
        /// <returns></returns>
        public Assignment AssignDriver(Guid driverId, IDriversActiveAssignment driversActiveAssignment)
        {
            CheckRule(new AssignmentAlreadyAssignedRule(this));
            CheckRule(new OneActiveAssignmentPerDriverRule(driverId, driversActiveAssignment));

            _driverId = driverId;
            _assignedOn = Clock.Now;
            return this;
        }

        /// <summary>
        /// Marks assignment as completed.
        /// </summary>
        /// <param name="driver">Given driver.</param>
        /// <returns></returns>
        public Assignment Complete(Guid driverId)
        {
            CheckRule(new AssignmentNotYetAssignedRule(this));
            CheckRule(new ValidAssignedDriverRule(_driverId.Value, driverId));
            CheckRule(new AssignmentAlreadyCompletedRule(this));

            _completedOn = Clock.Now;
            return this;
        }
    }
}
