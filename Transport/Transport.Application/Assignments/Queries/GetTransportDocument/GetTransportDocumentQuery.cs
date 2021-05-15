using BuildingBlocks.Application.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport.Application.Assignments.Queries.GetTransportDocument
{
    public class GetTransportDocumentQuery : IQuery<FileStreamResult>
    {
        public Guid AssignmentId { get; }

        public GetTransportDocumentQuery(Guid assignmentId)
        {
            AssignmentId = assignmentId;
        }
    }
}
