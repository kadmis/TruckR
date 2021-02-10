using Auth.Domain.Data.ValueObjects;
using Auth.Domain.Security.Passwords;

namespace Auth.Domain.Specifications.Password
{
    public class PasswordIsDifferent : IPasswordSpecification
    {
        private readonly IPasswordHasher _hasher;
        private readonly UserPassword _hashedPassword;

        public PasswordIsDifferent(IPasswordHasher hasher, UserPassword hashedPassword)
        {
            _hasher = hasher;
            _hashedPassword = hashedPassword;
        }

        public bool IsSatisfiedBy(UserPassword password)
        {
            return !_hasher.VerifyHashedPassword(_hashedPassword, password);
        }
    }
}
