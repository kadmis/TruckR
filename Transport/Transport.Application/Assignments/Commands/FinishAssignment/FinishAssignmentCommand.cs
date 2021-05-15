using BuildingBlocks.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport.Application.Assignments.Commands.FinishAssignment
{
    public class FinishAssignmentCommand : ICommand<FinishAssignmentResult>
    {
        public Guid AssignmentId { get; }

        public FinishAssignmentCommand(Guid assignmentId)
        {
            AssignmentId = assignmentId;
        }
    }
}
