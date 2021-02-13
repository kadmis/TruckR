using Auth.Domain.Data.ValueObjects;

namespace Auth.Domain.Specifications.PasswordSpecifications.Interfaces
{
    public interface IPasswordSpecification
    {
        public bool IsSatisfiedBy(Password password);
    }
}
