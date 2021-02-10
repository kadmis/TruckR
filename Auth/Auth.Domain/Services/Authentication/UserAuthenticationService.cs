using Auth.Domain.Data.Entities;
using Auth.Domain.Data.ValueObjects;
using Auth.Domain.Exceptions.AuthenticationExceptions;
using Auth.Domain.Exceptions.UserExceptions;
using Auth.Domain.Persistence;
using Auth.Domain.Security.Passwords;
using Auth.Domain.Specifications.Password;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Domain.Services.Authentication
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _hasher;

        public UserAuthenticationService(IUnitOfWork unitOfWork, IPasswordHasher hasher)
        {
            _unitOfWork = unitOfWork;
            _hasher = hasher;
        }

        public async Task<User> Authenticate(string username, string password, CancellationToken cancellationToken = default)
        {
            var givenUsername = new UserName(username);
            var givenPassword = new UserPassword(password);

            var user = await _unitOfWork.UserRepository.FindByUsername(givenUsername, cancellationToken);
            if(user == null)
            {
                //in authentication context it actually makes more sense to say that the given username is invalid instead of explicitly saying that the user doesn't exist
                throw new InvalidUsernameException(); 
            }
            if(user.Inactive)
            {
                throw new UserDeactivatedException();
            }
            if(user.IsDeleted)
            {
                throw new UserDeletedException();
            }

            var passwordIsDifferent = new PasswordMatches(_hasher, user.Password);
            if (!passwordIsDifferent.IsSatisfiedBy(givenPassword))
            {
                throw new InvalidPasswordException();
            }

            return user;
        }
    }
}
