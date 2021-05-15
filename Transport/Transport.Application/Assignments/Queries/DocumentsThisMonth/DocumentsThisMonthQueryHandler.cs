using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Handlers;
using BuildingBlocks.Domain;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Transport.Application.Assignments.Queries.DocumentsThisMonth
{
    public class DocumentsThisMonthQueryHandler : IQueryHandler<DocumentsThisMonthQuery, DocumentsThisMonthResult>
    {
        private readonly ISqlConnectionFactory _sqlConnection;

        public DocumentsThisMonthQueryHandler(ISqlConnectionFactory sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public async Task<DocumentsThisMonthResult> Handle(DocumentsThisMonthQuery request, CancellationToken cancellationToken)
        {
            var now = Clock.Now;
            var month = now.Month;
            var year = now.Year;

            var query = "SELECT " +
                "COUNT(1) " +
                "FROM dbo.Documents AS D" +
                "INNER JOIN dbo.Assignments A" +
                "ON A.DocumentId = D.Id " +
                "WHERE MONTH(A.CreatedDate) = @Month AND YEAR(A.CreatedDate) = @Year";

            try
            {
                var connection = _sqlConnection.GetOpenConnection();

                var result = await connection.QuerySingleAsync<long>(query, new
                {
                    Month = month,
                    Year = year
                });

                return DocumentsThisMonthResult.Success(result);
            }
            catch(Exception ex)
            {
                return DocumentsThisMonthResult.Fail(ex.Message);
            }
        }
    }
}
