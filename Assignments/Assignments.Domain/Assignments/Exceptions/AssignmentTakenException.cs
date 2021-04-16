using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport.Domain.Assignments.Exceptions
{
    public class AssignmentTakenException : Exception
    {
        public AssignmentTakenException():base("Assignment already has an assigned driver.")
        {
        }
    }
}
