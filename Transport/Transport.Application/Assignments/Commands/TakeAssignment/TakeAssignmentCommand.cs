using BuildingBlocks.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport.Application.Assignments.Commands.TakeAssignment
{
    public class TakeAssignmentCommand : ICommand<TakeAssignmentResult>
    {
        public Guid AssignmentId { get; }

        public TakeAssignmentCommand(Guid assignmentId)
        {
            AssignmentId = assignmentId;
        }
    }
}
