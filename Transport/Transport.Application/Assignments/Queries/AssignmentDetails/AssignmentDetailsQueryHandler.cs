using BuildingBlocks.Application.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Transport.Application.Assignments.Queries.AssignmentDetails
{
    public class AssignmentDetailsQueryHandler : IQueryHandler<AssignmentDetailsQuery, AssignmentDetailsResult>
    {
        public Task<AssignmentDetailsResult> Handle(AssignmentDetailsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
