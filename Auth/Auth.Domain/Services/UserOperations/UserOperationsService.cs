using Auth.Domain.Data.Entities;
using Auth.Domain.Data.ValueObjects;
using Auth.Domain.Exceptions;
using Auth.Domain.Exceptions.UserExceptions;
using Auth.Domain.Exceptions.UserNameExceptions;
using Auth.Domain.Persistence;
using Auth.Domain.Specifications.EmailSpecifications;
using Auth.Domain.Specifications.UsernameSpecifications;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Domain.Services.UserOperations
{
    public class UserOperationsService : IUserOperationsService
    {
        private readonly IUnitOfWork _uow;

        public UserOperationsService(IUnitOfWork uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<User> ChangeEmail(Guid id, string email, CancellationToken cancellationToken = default)
        {
            var user = await TryGetUser(id, cancellationToken);

            var newEmail = new Email(email);

            var emailExists = new EmailExistsOnOtherUsers(_uow)
                .Setup(id);

            if(await emailExists.IsSatisfiedBy(newEmail, cancellationToken))
            {
                throw new EmailExistsException();
            }

            user.ChangeEmail(newEmail);
            await _uow.Save(cancellationToken);

            return user;
        }

        public async Task<User> ResetPassword(Guid id, CancellationToken cancellationToken = default)
        {
            var user = await TryGetUser(id, cancellationToken);

            user.ResetPassword();
            await _uow.Save(cancellationToken);

            return user;
        }


        public async Task<User> SetPassword(Guid id, Guid resetToken, string password, CancellationToken cancellationToken = default)
        {
            var user = await TryGetUser(id, cancellationToken);

            user.SetPassword(new Password(password), resetToken);
            await _uow.Save(cancellationToken);

            return user;
        }

        public async Task<User> ChangeUsername(Guid id, string username, CancellationToken cancellationToken = default)
        {
            var user = await TryGetUser(id, cancellationToken);

            var newUsername = new Username(username);

            var usernameExists = new UsernameExistsOnOtherUsers(_uow);

            if (await usernameExists.Setup(id).IsSatisfiedBy(newUsername, cancellationToken))
            {
                throw new UsernameExistsException();
            }

            user.ChangeUsername(newUsername);
            await _uow.Save(cancellationToken);

            return user;
        }

        public async Task<User> Activate(Guid userId, Guid activationGuid, CancellationToken cancellationToken = default)
        {
            var user = await TryGetUser(userId, cancellationToken);

            user.Activate(activationGuid);
            await _uow.Save(cancellationToken);

            return user;
        }

        private async Task<User> TryGetUser(Guid id, CancellationToken cancellationToken = default)
        {
            var user = await _uow.UserRepository.FindById(id, cancellationToken);

            if (user == null)
            {
                throw new UserDoesntExistException();
            }

            return user;
        }
    }
}
