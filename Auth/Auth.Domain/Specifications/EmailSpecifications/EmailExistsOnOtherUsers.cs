using Auth.Domain.Data.ValueObjects;
using Auth.Domain.Exceptions.Specifications;
using Auth.Domain.Persistence;
using Auth.Domain.Specifications.EmailSpecifications.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Domain.Specifications.EmailSpecifications
{
    public class EmailExistsOnOtherUsers : IEmailExistsOnOtherUsers
    {
        private readonly IUnitOfWork _uow;
        private Guid? _currentUserId;

        public EmailExistsOnOtherUsers(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IEmailExistsOnOtherUsers Setup(Guid currentUserId)
        {
            _currentUserId = currentUserId;
            return this;
        }

        public async Task<bool> IsSatisfiedBy(Email email, CancellationToken cancellationToken = default)
        {
            if(_currentUserId == Guid.Empty || email == null)
            {
                throw new ArgumentsNotProvidedException(nameof(EmailExistsOnOtherUsers));
            }

            return await _uow.UserRepository.EmailExistsOnOtherUsers(email, _currentUserId.Value, cancellationToken);
        }
    }
}
