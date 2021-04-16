using Auth.Domain.Data.Entities;
using Auth.Domain.Data.ValueObjects;
using Auth.Domain.Exceptions;
using Auth.Domain.Exceptions.UsernameExceptions;
using Auth.Domain.Persistence;
using Auth.Domain.Specifications.EmailSpecifications.Interfaces;
using Auth.Domain.Specifications.UsernameSpecifications.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Domain.Services.Registration
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSpecification _emailExists;
        private readonly IUsernameSpecification _usernameExists;

        public UserRegistrationService(IUnitOfWork unitOfWork, IEmailExists emailExists, IUsernameExists usernameExists)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _emailExists = emailExists ?? throw new ArgumentNullException(nameof(emailExists));
            _usernameExists = usernameExists ?? throw new ArgumentNullException(nameof(usernameExists));
        }

        public async Task<User> RegisterDriver(string username, string firstname, string lastname, string password, string email, string phoneNumber, CancellationToken cancellationToken = default)
        {
            var user = User.Create(username, firstname, lastname, password, email, phoneNumber, UserRole.Driver);

            await ThrowIfEmailExists(user, cancellationToken);
            await ThrowIfUsernameExists(user, cancellationToken);

            _unitOfWork.UserRepository.Add(user);
            await _unitOfWork.Save(cancellationToken);

            return user;
        }

        public async Task<User> RegisterDispatcher(string username, string firstname, string lastname, string password, string email, string phoneNumber, CancellationToken cancellationToken = default)
        {
            var user = User.Create(username, firstname, lastname, password, email, phoneNumber, UserRole.Dispatcher);

            await ThrowIfEmailExists(user, cancellationToken);
            await ThrowIfUsernameExists(user, cancellationToken);

            _unitOfWork.UserRepository.Add(user);
            await _unitOfWork.Save(cancellationToken);

            return user;
        }

        private async Task ThrowIfEmailExists(User user, CancellationToken cancellationToken = default)
        {
            if(await _emailExists.IsSatisfiedBy(user.Email, cancellationToken))
            {
                throw new EmailExistsException();
            }
        }

        private async Task ThrowIfUsernameExists(User user, CancellationToken cancellationToken = default)
        {
            if (await _usernameExists.IsSatisfiedBy(user.Username, cancellationToken))
            {
                throw new UsernameExistsException();
            }
        }
    }
}
