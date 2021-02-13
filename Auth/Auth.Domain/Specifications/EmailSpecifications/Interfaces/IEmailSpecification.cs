using Auth.Domain.Data.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Domain.Specifications.EmailSpecifications.Interfaces
{
    public interface IEmailSpecification
    {
        public Task<bool> IsSatisfiedBy(Email email, CancellationToken cancellationToken = default);
    }
}
