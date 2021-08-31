using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Handlers;
using Dapper;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Transport.Application.Assignments.Queries.TransportDocumentInfo
{
    public class TransportDocumentInfoQueryHandler : IQueryHandler<TransportDocumentInfoQuery, TransportDocumentInfoResult>
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public TransportDocumentInfoQueryHandler(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<TransportDocumentInfoResult> Handle(TransportDocumentInfoQuery request, CancellationToken cancellationToken)
        {
            var query = "SELECT D.Name, D.Number " +
                "FROM dbo.Documents AS D " +
                "WHERE D.AssignmentId = @AssignmentId";

            try
            {
                var connection = _connectionFactory.GetOpenConnection();

                var result = await connection.QuerySingleAsync<TransportDocumentInfoDTO>(query, new { AssignmentId = request.AssignmentId });

                return TransportDocumentInfoResult.Success(result);
            }
            catch(Exception ex)
            {
                return TransportDocumentInfoResult.Fail(ex.Message);
            }
        }
    }
}
