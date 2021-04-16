using System;

namespace Transport.Domain.Assignments.Exceptions
{
    public class DriverAlreadyAssignedException : Exception
    {
        public DriverAlreadyAssignedException(Guid driverId) : base($"Driver with Id ({driverId}) is already assigned to a different assignment.")
        {

        }
    }
}
