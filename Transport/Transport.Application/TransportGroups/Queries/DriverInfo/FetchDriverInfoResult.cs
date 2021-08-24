using BuildingBlocks.Application.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport.Application.TransportGroups.Queries.DriverInfo
{
    public class FetchDriverInfoResult : IResult
    {
        public Guid GroupId { get; }

        public string Message { get; }

        public bool Successful { get; }
    }
}
