using Auth.Domain.Data.ValueObjects;
using Auth.Domain.Exceptions.Specifications;
using Auth.Domain.Persistence;
using Auth.Domain.Specifications.UsernameSpecifications.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Domain.Specifications.UsernameSpecifications
{
    public class UsernameExistsOnOtherUsers : IUsernameExistsOnOtherUsers
    {
        private readonly IUnitOfWork _uow;
        private Guid _currentUserId;

        public UsernameExistsOnOtherUsers(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IUsernameExistsOnOtherUsers Setup(Guid currentUserId)
        {
            _currentUserId = currentUserId;
            return this;
        }

        public async Task<bool> IsSatisfiedBy(Username username, CancellationToken cancellationToken = default)
        {
            if (username == null || _currentUserId == Guid.Empty)
            {
                throw new ArgumentsNotProvidedException(nameof(UsernameExistsOnOtherUsers));
            }

            return await _uow.UserRepository.UsernameExistsOnOtherUsers(username, _currentUserId, cancellationToken);
        }
    }
}
