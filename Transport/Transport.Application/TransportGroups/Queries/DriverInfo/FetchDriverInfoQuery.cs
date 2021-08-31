using BuildingBlocks.Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport.Application.TransportGroups.Queries.DriverInfo
{
    public class FetchDriverInfoQuery : IQuery<FetchDriverInfoResult>
    {
        public Guid DriverId { get; }

        public FetchDriverInfoQuery(Guid driverId)
        {
            DriverId = driverId;
        }
    }
}
