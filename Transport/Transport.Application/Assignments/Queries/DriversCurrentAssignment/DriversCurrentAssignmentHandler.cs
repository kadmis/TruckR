using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Handlers;
using BuildingBlocks.Application.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace Transport.Application.Assignments.Queries.DriversCurrentAssignment
{
    public class DriversCurrentAssignmentHandler : IQueryHandler<DriversCurrentAssignmentQuery, DriversCurrentAssignmentResult>
    {
        private readonly ISqlConnectionFactory _sqlConnection;

        public DriversCurrentAssignmentHandler(
            ISqlConnectionFactory sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public async Task<DriversCurrentAssignmentResult> Handle(DriversCurrentAssignmentQuery request, CancellationToken cancellationToken)
        {
            var query = "SELECT TOP 1 * " +
                "FROM dbo.Assignments AS A " +
                "WHERE A.DriverId = @DriverId AND A.CompletedOn IS NULL";

            try
            {
                var connection = _sqlConnection.GetOpenConnection();

                var result = await connection.QueryFirstAsync<AssignmentDetailsDTO>(query, new { request.DriverId });

                return DriversCurrentAssignmentResult.Success(result);
            }
            catch (Exception ex)
            {
                return DriversCurrentAssignmentResult.Fail(ex.Message);
            }
        }
    }
}
