using System;

namespace Transport.Domain.Assignments
{
    public interface IDriversActiveAssignment
    {
        public Guid? Get(Guid driverId);
    }
}
