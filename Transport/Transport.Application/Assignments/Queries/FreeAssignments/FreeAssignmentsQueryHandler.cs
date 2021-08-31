using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Handlers;
using Dapper;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Transport.Application.Assignments.Queries.FreeAssignments
{
    public class FreeAssignmentsQueryHandler : IQueryHandler<FreeAssignmentsQuery, FreeAssignmentsResult>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public FreeAssignmentsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<FreeAssignmentsResult> Handle(FreeAssignmentsQuery request, CancellationToken cancellationToken)
        {
            var query = "SELECT " +
                "A.Id, " +
                "A.Title, " +
                "(A.StartingStreet + ', ' + A.StartingPostalCode + ' ' + A.StartingCity + ', ' + A.StartingCountry) AS [From], " + 
                "(A.DestinationStreet + ', ' + A.DestinationPostalCode + ' ' + A.DestinationCity + ', ' + A.DestinationCountry) AS [To], " + 
                "A.CreatedOn AS CreatedDateUTC, " + 
                "A.Deadline AS DeadlineUTC " + 
                "FROM dbo.Assignments AS A " +
                "WHERE A.AssignedOn IS NULL AND A.CompletedOn IS NULL " +
                "ORDER BY A.CreatedOn DESC " +
                "OFFSET @Offset ROWS " + 
                "FETCH NEXT @PageSize ROWS ONLY; " +
                "SELECT COUNT(*) FROM dbo.Assignments AS A WHERE A.AssignedOn IS NULL AND A.CompletedOn IS NULL;";

            try
            {
                var connection = _sqlConnectionFactory.GetOpenConnection();

                var results = await connection.QueryMultipleAsync(query, new
                {
                    @Offset = (request.Page - 1) * request.PageSize,
                    @PageSize = request.PageSize
                });

                var items = await results.ReadAsync<AssignmentListDTO>();
                var totalItems = await results.ReadFirstAsync<int>();

                return FreeAssignmentsResult.Success(items, totalItems);
            }
            catch(Exception ex)
            {
                return FreeAssignmentsResult.Fail(ex.Message);
            }
        }
    }
}
