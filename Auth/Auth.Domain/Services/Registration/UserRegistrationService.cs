using Auth.Domain.Creation;
using Auth.Domain.Data.Entities;
using Auth.Domain.Exceptions;
using Auth.Domain.Exceptions.UserNameExceptions;
using Auth.Domain.Persistence;
using Auth.Domain.Specifications.Email;
using Auth.Domain.Specifications.Username;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Domain.Services.Registration
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserFactory _userFactory;

        public UserRegistrationService(IUnitOfWork unitOfWork, IUserFactory userFactory)
        {
            _unitOfWork = unitOfWork;
            _userFactory = userFactory;
        }

        public async Task<User> Register(string username, string password, string email, CancellationToken cancellationToken = default)
        {
            var user = _userFactory.Create(username, password, email);

            await ThrowIfEmailExists(user, cancellationToken);
            await ThrowIfUsernameExists(user, cancellationToken);

            _unitOfWork.UserRepository.Add(user);
            await _unitOfWork.Save();

            return user;
        }

        private async Task ThrowIfEmailExists(User user, CancellationToken cancellationToken = default)
        {
            var emailExists = new EmailExists(_unitOfWork);

            if(await emailExists.IsSatisfiedBy(user.Email, cancellationToken))
            {
                throw new EmailExistsException();
            }
        }

        private async Task ThrowIfUsernameExists(User user, CancellationToken cancellationToken = default)
        {
            var usernameExists = new UsernameExists(_unitOfWork);

            if (await usernameExists.IsSatisfiedBy(user.Username, cancellationToken))
            {
                throw new UsernameExistsException();
            }
        }
    }
}
