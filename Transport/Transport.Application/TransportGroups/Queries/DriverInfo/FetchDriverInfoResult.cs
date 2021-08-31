using BuildingBlocks.Application.Models.Results;
using System;

namespace Transport.Application.TransportGroups.Queries.DriverInfo
{
    public class FetchDriverInfoResult : IResult
    {
        public Guid? GroupId { get; private set; }

        public string Message { get; private set; }

        public bool Successful { get; private set; }

        private FetchDriverInfoResult()
        {
        }

        public static FetchDriverInfoResult Success(Guid? groupId)
        {
            return new FetchDriverInfoResult
            {
                GroupId = groupId,
                Successful = true,
                Message = string.Empty
            };
        }
        public static FetchDriverInfoResult Fail(string message)
        {
            return new FetchDriverInfoResult
            {
                Successful = false,
                Message = message
            };
        }
    }
}
