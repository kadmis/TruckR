using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Handlers;
using System;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Auth.Domain.Security.Encryption;

namespace Auth.Application.Queries.UsersDetails
{
    public class UsersDetailsQueryHandler : IQueryHandler<UsersDetailsQuery, UsersDetailsResult>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IEncryptor _decryptor;

        public UsersDetailsQueryHandler(
            ISqlConnectionFactory sqlConnectionFactory, 
            IEncryptor decryptor)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _decryptor = decryptor;
        }

        public async Task<UsersDetailsResult> Handle(UsersDetailsQuery request, CancellationToken cancellationToken)
        {
            var query = "SELECT " +
                "U.FirstName, " +
                "U.LastName, " +
                "U.Email, " +
                "U.Phone " +
                "FROM dbo.Users AS U " +
                "WHERE U.Id IN @UserIds";

            try
            {
                var connection = _sqlConnectionFactory.GetOpenConnection();

                var results = await connection.QueryAsync<UserDetailsDTO>(query, new { UserIds = request.Ids });

                foreach (var result in results)
                    result.Decrypt(_decryptor);

                return UsersDetailsResult.Success(results);
            }
            catch(Exception ex)
            {
                return UsersDetailsResult.Fail(ex.Message);
            }
        }
    }
}
