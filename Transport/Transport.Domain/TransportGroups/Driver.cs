using BuildingBlocks.Domain;
using System;

namespace Transport.Domain.Groups
{
    public class Driver : Entity
    {
        public Guid Id { get; private set; }

        public Guid GroupId { get; private set; }

        private Driver()
        {
        }

        internal static Driver Create(Guid id, Guid groupId)
        {
            return new Driver
            {
                Id = id,
                GroupId = groupId
            };
        }
    }
}
