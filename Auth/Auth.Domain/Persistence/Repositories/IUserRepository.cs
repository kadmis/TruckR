using Auth.Domain.Data.Entities;
using Auth.Domain.Data.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Domain.Persistence.Repositories
{
    public interface IUserRepository
    {
        Guid Add(User user);
        Task Delete(Guid id, CancellationToken cancellationToken = default);
        Task<User> FindByUsername(UserName username, CancellationToken cancellationToken = default);
        Task<User> FindById(Guid id, CancellationToken cancellationToken = default);
        Task<User> FindByEmail(UserEmail email, CancellationToken cancellationToken = default);
        Task<bool> UsernameExists(UserName username, CancellationToken cancellationToken = default);
        Task<bool> EmailExists(UserEmail email, CancellationToken cancellationToken = default);
        Task<bool> UsernameExistsOnOtherUsers(UserName username, Guid currentUserId, CancellationToken cancellationToken = default);
        Task<bool> EmailExistsOnOtherUsers(UserEmail email, Guid currentUserId, CancellationToken cancellationToken = default);
    }
}
