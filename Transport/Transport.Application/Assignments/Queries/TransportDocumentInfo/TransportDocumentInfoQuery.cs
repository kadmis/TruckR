using BuildingBlocks.Application.Queries;
using System;

namespace Transport.Application.Assignments.Queries.TransportDocumentInfo
{
    public class TransportDocumentInfoQuery : IQuery<TransportDocumentInfoResult>
    {
        public Guid AssignmentId { get; }

        public TransportDocumentInfoQuery(Guid assignmentId)
        {
            AssignmentId = assignmentId;
        }
    }
}
