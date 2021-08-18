using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using Transport.Domain.TransportGroups.Rules;

namespace Transport.Domain.Groups
{
    public class TransportGroup : Entity, IAggregateRoot
    {
        public const int DriverLimit = 15;

        public Guid Id { get; private set; }

        private Guid _dispatcherId;

        private readonly List<Driver> _drivers = new();

        public IEnumerable<Driver> Drivers => _drivers.AsReadOnly();

        private TransportGroup()
        {
        }

        public static TransportGroup Create(Guid dispatcherId)
        {
            return new TransportGroup
            {
                Id = Guid.NewGuid(),
                _dispatcherId = dispatcherId,
            };
        }

        public TransportGroup AddDriver(Guid id)
        {
            CheckRule(new GroupCannotHaveMoreThanGivenNumberOfDrivers(this, DriverLimit));

            _drivers.Add(Driver.Create(id, Id));

            return this;
        }
    }
}
