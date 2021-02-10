using Auth.Domain.Data.ValueObjects;
using Auth.Domain.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Domain.Specifications.Email
{
    public class EmailExistsOnOtherUsers : IEmailSpecification
    {
        private readonly IUnitOfWork _uow;
        private readonly Guid _currentUserId;

        public EmailExistsOnOtherUsers(IUnitOfWork uow, Guid currentUserId)
        {
            _uow = uow;
            _currentUserId = currentUserId;
        }

        public async Task<bool> IsSatisfiedBy(UserEmail email, CancellationToken cancellationToken = default)
        {
            return await _uow.UserRepository.EmailExistsOnOtherUsers(email, _currentUserId, cancellationToken);
        }
    }
}
