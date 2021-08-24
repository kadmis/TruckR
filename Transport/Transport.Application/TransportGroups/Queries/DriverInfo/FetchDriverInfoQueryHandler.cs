using BuildingBlocks.Application.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Transport.Application.TransportGroups.Queries.DriverInfo
{
    public class FetchDriverInfoQueryHandler : IQueryHandler<FetchDriverInfoQuery, FetchDriverInfoResult>
    {
        public Task<FetchDriverInfoResult> Handle(FetchDriverInfoQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
