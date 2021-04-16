using Transport.Domain.Assignments.Exceptions;
using BuildingBlocks.Domain;
using System;
using System.Threading.Tasks;
using Transport.Domain.Employees;

namespace Transport.Domain.Assignments
{
    public class Assignment : IEntity<long>, IAggregateRoot
    {
        //Immutables
        public long Id { get; }

        private string _title;

        private string _description;

        private Guid _transportDocumentId;

        private Guid _dispatcherId;

        private Address _destination;

        private DateTime _deadline;

        private DateTime _createdOn;

        private double _requiredLoad;

        //Mutables
        private Guid? _driverId;

        private DateTime? _assignedOn;

        private DateTime? _completedOn;

        public bool Assigned => _assignedOn.HasValue && _driverId.HasValue;
        public bool Completed => _completedOn.HasValue;

        private Assignment()
        {
        }
        private Assignment(
            string title, 
            string description, 
            Document transportDocument, 
            Employee dispatcher, 
            DateTime deadline,
            Address address)
        {
            _title = title;
            _description = description;
            _transportDocumentId = transportDocument.Id;
            _dispatcherId = dispatcher.Id;
            _deadline = deadline;
            _destination = address;
            _createdOn = DateTime.UtcNow;
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
            Employee dispatcher, 
            DateTime deadline,
            string city, string street, string postalCode)
        {
            return new Assignment(
                title,
                description,
                transportDocument,
                dispatcher,
                deadline,
                Address.Create(city, street, postalCode));
        }

        /// <summary>
        /// Assigns a driver to this assignment.
        /// </summary>
        /// <param name="driver">Given driver.</param>
        /// <returns></returns>
        public Assignment AssignDriver(Employee driver)
        {
            if (driver.Id.Equals(_dispatcherId))
            {
                throw new DriverIsTheDispatcherException(driver.Id);
            }
            if(Assigned)
            {
                throw new AssignmentTakenException();
            }

            _driverId = driver.Id;
            _assignedOn = DateTime.UtcNow;
            return this;
        }

        /// <summary>
        /// Completes an assignment.
        /// </summary>
        /// <returns></returns>
        public Assignment Complete()
        {
            if(!Assigned)
            {
                throw new AssignmentNoAssignedException();
            }

            _completedOn = DateTime.UtcNow;
            return this;
        }
    }
}
