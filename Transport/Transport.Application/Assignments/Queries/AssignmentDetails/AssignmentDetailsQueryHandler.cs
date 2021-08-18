using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Handlers;
using Dapper;
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
        private readonly ISqlConnectionFactory _sqlConnection;

        public AssignmentDetailsQueryHandler(ISqlConnectionFactory sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public async Task<AssignmentDetailsResult> Handle(AssignmentDetailsQuery request, CancellationToken cancellationToken)
        {
            var query = "SELECT " +
                "A.Title, " +
                "A.Description, " +
                "A.DestinationCountry, " +
                "A.DestinationCity, " +
                "A.DestinationStreet, " +
                "A.DestinationPostalCode, " +
                "A.StartingCountry, " +
                "A.StartingCity, " +
                "A.StartingStreet, " +
                "A.StartingPostalCode, " +
                "A.Deadline, " + 
                "A.CreatedOn, " +
                "A.CompletedOn, " +
                "A.AssignedOn, " +
                "A.Dispatcher, " +
                "A.Driver " +
                "FROM dbo.v_AssignmentDetails AS A" +
                "WHERE A.Id = @AssignmentId";

            try
            {
                var connection = _sqlConnection.GetOpenConnection();

                var result = await connection.QuerySingleAsync<AssignmentDetailsDTO>(query, new
                {
                    AssignmentId = request.AssignmentId
                });

                return AssignmentDetailsResult.Success(result);
            }
            catch(Exception ex)
            {
                return AssignmentDetailsResult.Fail(ex.Message);
            }
        }
    }
}
