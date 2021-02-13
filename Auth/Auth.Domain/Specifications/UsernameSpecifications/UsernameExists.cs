using Auth.Domain.Data.ValueObjects;
using Auth.Domain.Exceptions.Specifications;
using Auth.Domain.Persistence;
using Auth.Domain.Specifications.UsernameSpecifications.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Domain.Specifications.UsernameSpecifications
{
    public class UsernameExists : IUsernameExists
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsernameExists(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> IsSatisfiedBy(Username username, CancellationToken cancellationToken = default)
        {
            if(username == null)
            {
                throw new ArgumentsNotProvidedException(nameof(UsernameExists));
            }

            return await _unitOfWork.UserRepository.UsernameExists(username, cancellationToken);
        }
    }
}
