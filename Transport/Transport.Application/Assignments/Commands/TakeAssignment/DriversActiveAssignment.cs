using BuildingBlocks.Application.Data;
using Dapper;
using System;
using Transport.Domain.Assignments;

namespace Transport.Application.Assignments.Commands.TakeAssignment
{
    public class DriversActiveAssignment : IDriversActiveAssignment
    {
        private readonly ISqlConnectionFactory _sqlConnection;

        public DriversActiveAssignment(ISqlConnectionFactory sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public Guid? Get(Guid driverId)
        {
            var query = "SELECT A.Id " +
                "FROM dbo.Assignments AS A " +
                "WHERE A.DriverId = @DriverId AND A.CompletedOn IS NULL";

            var connection = _sqlConnection.GetOpenConnection();

            return connection.QueryFirstOrDefault<Guid?>(query, new { DriverId = driverId });
        }
    }
}
