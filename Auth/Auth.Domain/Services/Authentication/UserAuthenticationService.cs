using Auth.Domain.Data.Entities;
using Auth.Domain.Data.ValueObjects;
using Auth.Domain.Exceptions.AuthenticationExceptions;
using Auth.Domain.Exceptions.UserExceptions;
using Auth.Domain.Persistence;
using Auth.Domain.Specifications.PasswordSpecifications.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Domain.Services.Authentication
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordMatches _passwordMatches;

        public UserAuthenticationService(IUnitOfWork unitOfWork, IPasswordMatches passwordMatches)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _passwordMatches = passwordMatches ?? throw new ArgumentNullException(nameof(passwordMatches));
        }

        public async Task<User> Authenticate(string username, string password, CancellationToken cancellationToken = default)
        {
            var givenUsername = new Username(username);
            var givenPassword = new Password(password);

            var user = await _unitOfWork.UserRepository.FindByUsername(givenUsername, cancellationToken);

            ThrowIfUserDoesntExist(user);

            ThrowIfUserIsInactive(user);

            ThrowIfUserIsDeleted(user);

            ThrowIfPasswordIsIncorrect(user, givenPassword);

            return user;
        }

        private void ThrowIfUserDoesntExist(User user)
        {
            if (user == null)
            {
                //in authentication context it actually makes more sense to say that the given username is invalid instead of explicitly saying that the user doesn't exist
                throw new InvalidUsernameException();
            }
        }

        private void ThrowIfUserIsInactive(User user)
        {
            if (user.Inactive)
            {
                throw new UserInactiveException();
            }
        }

        private void ThrowIfUserIsDeleted(User user)
        {
            if (user.IsDeleted)
            {
                throw new UserDeletedException();
            }
        }

        private void ThrowIfPasswordIsIncorrect(User user, Password givenPassword)
        {
            if (!_passwordMatches.Setup(user.Password).IsSatisfiedBy(givenPassword))
            {
                throw new InvalidPasswordException();
            }
        }
    }
}
