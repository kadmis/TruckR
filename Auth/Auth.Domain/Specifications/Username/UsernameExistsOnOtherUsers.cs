using Auth.Domain.Data.ValueObjects;
using Auth.Domain.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Domain.Specifications.Username
{
    public class UsernameExistsOnOtherUsers : IUsernameSpecification
    {
        private readonly IUnitOfWork _uow;
        private readonly Guid _currentUserId;

        public UsernameExistsOnOtherUsers(IUnitOfWork uow, Guid currentUserId)
        {
            _uow = uow;
            _currentUserId = currentUserId;
        }

        public async Task<bool> IsSatisfiedBy(UserName username, CancellationToken cancellationToken = default)
        {
            return await _uow.UserRepository.UsernameExistsOnOtherUsers(username, _currentUserId, cancellationToken);
        }
    }
}
