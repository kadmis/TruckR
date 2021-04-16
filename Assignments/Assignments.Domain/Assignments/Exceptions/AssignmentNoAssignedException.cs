using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport.Domain.Assignments.Exceptions
{
    public class AssignmentNoAssignedException : Exception
    {
        public AssignmentNoAssignedException():base("Assignment not yet assigned.")
        {
        }
    }
}
