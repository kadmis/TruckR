using Auth.Domain.Data.ValueObjects;
using Auth.Domain.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Domain.Specifications.Username
{
    public class UsernameExists : IUsernameSpecification
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsernameExists(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> IsSatisfiedBy(UserName username, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.UserRepository.UsernameExists(username, cancellationToken);    
        }
    }
}
