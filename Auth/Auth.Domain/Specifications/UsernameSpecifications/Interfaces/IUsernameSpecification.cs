using Auth.Domain.Data.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Domain.Specifications.UsernameSpecifications.Interfaces
{
    public interface IUsernameSpecification
    {
        public Task<bool> IsSatisfiedBy(Username username, CancellationToken cancellationToken = default);
    }
}
