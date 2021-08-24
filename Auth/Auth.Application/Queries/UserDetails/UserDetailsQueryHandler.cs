using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Handlers;
using BuildingBlocks.Application.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Auth.Domain.Security.Encryption;

namespace Auth.Application.Queries.UserDetails
{
    public class UserDetailsQueryHandler : IQueryHandler<UserDetailsQuery, UserDetailsResult>
    {
        private readonly ISqlConnectionFactory _sqlConnection;
        private readonly IIdentityAccessor _identity;
        private readonly IEncryptor _decryptor;

        public UserDetailsQueryHandler(
            ISqlConnectionFactory sqlConnection,
            IIdentityAccessor identity, 
            IEncryptor decryptor)
        {
            _sqlConnection = sqlConnection;
            _identity = identity;
            _decryptor = decryptor;
        }

        public async Task<UserDetailsResult> Handle(UserDetailsQuery request, CancellationToken cancellationToken)
        {
            if (!request.UserId.HasValue)
                request.UserId = _identity.UserIdentity().UserId;

            var query = "SELECT " +
                "U.FirstName, " +
                "U.LastName, " +
                "U.Email, " +
                "U.Phone " +
                "FROM dbo.Users AS U " +
                "WHERE U.Id = @UserId";

            try
            {
                var connection = _sqlConnection.GetOpenConnection();

                var dto = await connection.QuerySingleAsync<UserDetailsDTO>(query, new 
                { 
                    UserId = request.UserId 
                });

                return UserDetailsResult.Success(dto.Decrypt(_decryptor));
            }
            catch(Exception ex)
            {
                return UserDetailsResult.Fail(ex.Message);
            }
        }
    }
}
