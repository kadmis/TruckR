using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport.Domain.Assignments.Exceptions
{
    public class DriverIsTheDispatcherException : Exception
    {
        public DriverIsTheDispatcherException(Guid driverId) : base($"User with given Id ({driverId}) is this assignments dispatcher")
        {
        }
    }
}
