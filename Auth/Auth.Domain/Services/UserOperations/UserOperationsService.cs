using Auth.Domain.Data.Entities;
using Auth.Domain.Data.ValueObjects;
using Auth.Domain.Exceptions;
using Auth.Domain.Exceptions.UserExceptions;
using Auth.Domain.Exceptions.UserNameExceptions;
using Auth.Domain.Persistence;
using Auth.Domain.Specifications.Email;
using Auth.Domain.Specifications.Username;
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
            _uow = uow;
        }

        public async Task<User> ChangeEmail(Guid id, string email, CancellationToken cancellationToken = default)
        {
            var user = await _uow.UserRepository.FindById(id, cancellationToken);

            if(user == null)
            {
                throw new UserDoesntExistException();
            }

            var newEmail = new UserEmail(email);

            var emailExists = new EmailExistsOnOtherUsers(_uow, id);

            if(await emailExists.IsSatisfiedBy(newEmail))
            {
                throw new EmailExistsException();
            }

            user.ChangeEmail(newEmail);
            await _uow.Save(cancellationToken);

            return user;
        }

        public async Task<User> ResetPassword(Guid id, CancellationToken cancellationToken = default)
        {
            var user = await _uow.UserRepository.FindById(id, cancellationToken);

            if (user == null)
            {
                throw new UserDoesntExistException();
            }

            user.ResetPassword();
            await _uow.Save(cancellationToken);

            return user;
        }

        public async Task<User> ChangeUsername(Guid id, string username, CancellationToken cancellationToken = default)
        {
            var user = await _uow.UserRepository.FindById(id, cancellationToken);

            if (user == null)
            {
                throw new UserDoesntExistException();
            }

            var newUsername = new UserName(username);

            var usernameExists = new UsernameExistsOnOtherUsers(_uow, id);

            if (await usernameExists.IsSatisfiedBy(newUsername))
            {
                throw new UsernameExistsException();
            }

            user.ChangeUsername(newUsername);
            await _uow.Save(cancellationToken);

            return user;
        }

        public async Task<User> Activate(Guid userId, Guid activationGuid, CancellationToken cancellationToken = default)
        {
            var user = await _uow.UserRepository.FindById(userId, cancellationToken);

            if(user == null)
            {
                throw new UserDoesntExistException();
            }

            user.Activate(activationGuid);
            await _uow.Save(cancellationToken);

            return user;
        }
    }
}
