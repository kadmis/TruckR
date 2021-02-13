using Auth.Domain.Data.ValueObjects;
using Auth.Domain.Exceptions.Specifications;
using Auth.Domain.Persistence;
using Auth.Domain.Specifications.EmailSpecifications.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Domain.Specifications.EmailSpecifications
{
    public class EmailExists : IEmailExists
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmailExists(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<bool> IsSatisfiedBy(Email email, CancellationToken cancellationToken = default)
        {
            if(email == null)
            {
                throw new ArgumentsNotProvidedException(nameof(EmailExists));
            }

            return _unitOfWork.UserRepository.EmailExists(email, cancellationToken);
        }
    }
}
