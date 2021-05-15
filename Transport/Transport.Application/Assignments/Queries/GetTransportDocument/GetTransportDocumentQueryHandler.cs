using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Handlers;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using Transport.Application.Files;

namespace Transport.Application.Assignments.Queries.GetTransportDocument
{
    public class GetTransportDocumentQueryHandler : IQueryHandler<GetTransportDocumentQuery, FileStreamResult>
    {
        private readonly ISqlConnectionFactory _sqlConnection;
        private readonly IFilesStorage _filesStorage;

        public GetTransportDocumentQueryHandler(
            ISqlConnectionFactory sqlConnection, 
            IFilesStorage filesStorage)
        {
            _sqlConnection = sqlConnection;
            _filesStorage = filesStorage;
        }

        public async Task<FileStreamResult> Handle(GetTransportDocumentQuery request, CancellationToken cancellationToken)
        {
            var query = "SELECT " +
                "D.Id, " +
                "D.Name " +
                "FROM dbo.Documents AS D" +
                "INNER JOIN dbo.Assignments A" +
                "ON A.DocumentId = D.Id " +
                "WHERE A.Id = @AssignmentId";

            try
            {
                var connection = _sqlConnection.GetOpenConnection();

                var result = await connection.QuerySingleAsync<DocumentDTO>(query, new
                {
                    AssignmentId = request.AssignmentId
                });

                var file = await _filesStorage.Read(result.Id.ToString(), result.Name, cancellationToken);

                return new FileStreamResult(file.ContentStream, file.ContentType);
            }
            catch(Exception)
            {
                return null;
            }
        }

        internal class DocumentDTO
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }
    }
}
