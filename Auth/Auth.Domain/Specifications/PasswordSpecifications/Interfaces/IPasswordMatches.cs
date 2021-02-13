using Auth.Domain.Data.ValueObjects;

namespace Auth.Domain.Specifications.PasswordSpecifications.Interfaces
{
    public interface IPasswordMatches : IPasswordSpecification
    {
        public IPasswordMatches Setup(Password hashedPassword);
    }
}
