using Auth.Domain.Data.ValueObjects;
using Auth.Domain.Persistence;
using Auth.Domain.Persistence.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Domain.Specifications.Email
{
    public class EmailExists : IEmailSpecification
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmailExists(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> IsSatisfiedBy(UserEmail email, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.UserRepository.EmailExists(email, cancellationToken);
        }
    }
}
