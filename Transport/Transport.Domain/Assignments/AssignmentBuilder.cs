using System;
using Transport.Domain.Dispatchers;
using Transport.Domain.Documents;

namespace Transport.Domain.Assignments
{
    public class AssignmentBuilder : IAssignmentBuilder
    {
        private string _title;
        private string _description;
        private DateTime _deadline;

        private Dispatcher _dispatcher;

        private Document _transportDocument;

        private Address _start;
        private Address _destination;

        public IAssignmentBuilder AddBasicInformation(string title, string description, DateTime deadline)
        {
            _title = title;
            _description = description;
            _deadline = deadline;

            return this;
        }

        public IAssignmentBuilder AddDispatcher(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;

            return this;
        }

        public IAssignmentBuilder AddTransportDocument(string name, long ordinalNumber)
        {
            _transportDocument = Document.Create(name, ordinalNumber);

            return this;
        }

        public IAssignmentBuilder AddDestination(string country, string city, string street, string postalCode)
        {
            _destination = Address.Create(country, city, street, postalCode);

            return this;
        }

        public IAssignmentBuilder AddStartingPoint(string country, string city, string street, string postalCode)
        {
            _start = Address.Create(country, city, street, postalCode);

            return this;
        }

        public Assignment Build()
        {
            return Assignment.Create(
                _title, _description,
                _transportDocument, _dispatcher,
                _deadline,
                _start,
                _destination);
        }

    }
}
