using Auth.Domain.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Domain.Services.UserOperations
{
    public interface IUserOperationsService
    {
        public Task<User> ChangeUsername(Guid id, string username, CancellationToken cancellationToken = default);
        public Task<User> ChangeEmail(Guid id, string email, CancellationToken cancellationToken = default);
        public Task<User> ResetPassword(Guid id, CancellationToken cancellationToken = default);
        public Task<User> Activate(Guid userId, Guid activationGuid, CancellationToken cancellationToken = default);
    }
}
