using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Handlers;
using System;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace Transport.Application.TransportGroups.Queries.DriverInfo
{
    public class FetchDriverInfoQueryHandler : IQueryHandler<FetchDriverInfoQuery, FetchDriverInfoResult>
    {
        private readonly ISqlConnectionFactory _sqlConnection;

        public FetchDriverInfoQueryHandler(ISqlConnectionFactory sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public async Task<FetchDriverInfoResult> Handle(FetchDriverInfoQuery request, CancellationToken cancellationToken)
        {
            var query = "SELECT D.GroupId FROM dbo.Drivers AS D WHERE D.Id = @DriverId";

            try
            {
                var connection = _sqlConnection.GetOpenConnection();

                var result = await connection.QueryFirstAsync<Guid?>(query, new { DriverId = request.DriverId });

                return FetchDriverInfoResult.Success(result);
            }
            catch(Exception ex)
            {
                return FetchDriverInfoResult.Fail(ex.Message);
            }
        }
    }
}
