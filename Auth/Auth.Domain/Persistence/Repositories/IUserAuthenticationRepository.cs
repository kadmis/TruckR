using Auth.Domain.Data.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Domain.Persistence.Repositories
{
    public interface IUserAuthenticationRepository
    {
        public Task<UserAuthentication> FindById(Guid id, CancellationToken cancellationToken = default);
        public UserAuthentication Add(UserAuthentication userAuthentication);
    }
}
