using Auth.Domain.Data.ValueObjects;

namespace Auth.Domain.Security.Passwords
{
    public interface IPasswordHasher
    {
        public string HashPassword(string password);
        public bool VerifyHashedPassword(UserPassword hashedPassword, string providedPassword);
        public bool VerifyHashedPassword(UserPassword hashedPassword, UserPassword providedPassword);
    }
}
