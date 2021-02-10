using Auth.Domain.Data.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Domain.Specifications.Email
{
    public interface IEmailSpecification
    {
        public Task<bool> IsSatisfiedBy(UserEmail email, CancellationToken cancellationToken = default);
    }
}
