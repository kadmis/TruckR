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
        Task<User> FindByUsername(Username username, CancellationToken cancellationToken = default);
        Task<User> FindById(Guid id, CancellationToken cancellationToken = default);
        Task<User> FindByEmail(Email email, CancellationToken cancellationToken = default);
        Task<User> FindByEmailAndUsername(Email email, Username username, CancellationToken cancellationToken = default);
        Task<bool> UsernameExists(Username username, CancellationToken cancellationToken = default);
        Task<bool> EmailExists(Email email, CancellationToken cancellationToken = default);
        Task<bool> UsernameExistsOnOtherUsers(Username username, Guid currentUserId, CancellationToken cancellationToken = default);
        Task<bool> EmailExistsOnOtherUsers(Email email, Guid currentUserId, CancellationToken cancellationToken = default);
    }
}
