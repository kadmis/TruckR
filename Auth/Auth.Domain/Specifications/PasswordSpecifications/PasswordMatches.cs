using Auth.Domain.Data.ValueObjects;
using Auth.Domain.Exceptions.Specifications;
using Auth.Domain.Security.Passwords;
using Auth.Domain.Specifications.PasswordSpecifications.Interfaces;

namespace Auth.Domain.Specifications.PasswordSpecifications
{
    public class PasswordMatches : IPasswordMatches
    {
        private readonly IPasswordHasher _hasher;
        private Password _hashedPassword;

        public PasswordMatches(IPasswordHasher hasher)
        {
            _hasher = hasher;
        }

        public IPasswordMatches Setup(Password hashedPassword)
        {
            _hashedPassword = hashedPassword;
            return this;
        }

        public bool IsSatisfiedBy(Password password)
        {
            if (_hashedPassword == null || password == null)
            {
                throw new ArgumentsNotProvidedException(nameof(PasswordMatches));
            }

            return _hasher.VerifyHashedPassword(_hashedPassword, password);
        }
    }
}
