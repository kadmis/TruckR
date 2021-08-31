using BuildingBlocks.Application.Queries;
using System;

namespace Transport.Application.Assignments.Queries.AssignmentDetails
{
    public class AssignmentDetailsQuery : IQuery<AssignmentDetailsResult>
    {
        public Guid? AssignmentId { get; }

        public AssignmentDetailsQuery(Guid assignmentId)
        {
            AssignmentId = assignmentId;
        }
    }
}
