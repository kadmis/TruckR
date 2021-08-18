using Auth.Domain.Data.Entities;
using Auth.Domain.Persistence.Repositories;
using Auth.Infrastructure.Persistence.Context;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Persistence.Repositories
{
    public class UserAuthenticationRepository : IUserAuthenticationRepository
    {
        private readonly AuthContext _context;

        public UserAuthenticationRepository(AuthContext context)
        {
            _context = context;
        }

        public UserAuthentication Add(UserAuthentication userAuthentication)
        {
            return _context.UserAuthentications.Add(userAuthentication).Entity;
        }

        public async Task<UserAuthentication> FindById(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.UserAuthentications.FindAsync(new object[] { id }, cancellationToken);
        }
    }
}
