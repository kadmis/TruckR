using Auth.Domain.Data.Entities;
using Auth.Domain.Data.ValueObjects;
using Auth.Domain.Security.Passwords;
using Microsoft.AspNetCore.Identity;

namespace Auth.Infrastructure.Security.Passwords
{
    public class PasswordHasher : IPasswordHasher
    {
        private readonly IPasswordHasher<User> _hasher;

        public PasswordHasher(IPasswordHasher<User> hasher)
        {
            _hasher = hasher;
        }
        public PasswordHasher()
        {
            _hasher = new PasswordHasher<User>();
        }

        public string HashPassword(string password)
        {
            return _hasher.HashPassword(null, password);
        }

        public bool VerifyHashedPassword(Password hashedPassword, string providedPassword)
        {
            var result = _hasher.VerifyHashedPassword(null, hashedPassword.Value, providedPassword);
            return result is PasswordVerificationResult.Success;
        }

        public bool VerifyHashedPassword(Password hashedPassword, Password providedPassword)
        {
            var result = _hasher.VerifyHashedPassword(null, hashedPassword.Value, providedPassword.Value);
            return result is PasswordVerificationResult.Success;
        }
    }
}
