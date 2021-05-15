using BuildingBlocks.Domain;
using System;
using Transport.Domain.Dispatchers;
using Transport.Domain.Drivers;
using Transport.Domain.Documents;

namespace Transport.Domain.Assignments
{
    public class Assignment : IEntity<Guid>, IAggregateRoot
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
            Dispatcher dispatcher, 
            DateTime deadline,
            Address start,
            Address destination)
        {
            Id = Guid.NewGuid();
            _title = title;
            _description = description;
            _transportDocument = transportDocument;
            _dispatcherId = dispatcher.Id;
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
            Dispatcher dispatcher, 
            DateTime deadline,
            Address start,
            Address destination)
        {
            return new Assignment(
                title,
                description,
                transportDocument,
                dispatcher,
                deadline,
                start,
                destination);
        }

        /// <summary>
        /// Assigns a driver to this assignment.
        /// </summary>
        /// <param name="driver">Given driver.</param>
        /// <returns></returns>
        public Assignment AssignDriver(Driver driver)
        {
            if (driver.Id.Equals(_dispatcherId))
            {
                throw new ArgumentException("Dispatcher cannot be the driver.");
            }
            if(Assigned)
            {
                throw new InvalidOperationException("Assignment is already taken.");
            }

            _driverId = driver.Id;
            _assignedOn = Clock.Now;
            return this;
        }

        /// <summary>
        /// Marks assignment as completed.
        /// </summary>
        /// <param name="driver">Given driver.</param>
        /// <returns></returns>
        public Assignment Complete(Driver driver)
        {
            if(!Assigned)
            {
                throw new InvalidOperationException("Assignment not yet assigned.");
            }
            if(!driver.Id.Equals(_driverId))
            {
                throw new ArgumentException("Invalid driver.");
            }
            if(Completed)
            {
                throw new InvalidOperationException("Assignment is already completed.");
            }

            _completedOn = Clock.Now;
            return this;
        }
    }
}
