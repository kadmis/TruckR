using Auth.Domain.Data.ValueObjects;

namespace Auth.Domain.Security.Passwords
{
    public interface IPasswordHasher
    {
        public string HashPassword(string password);
        public bool VerifyHashedPassword(Password hashedPassword, string providedPassword);
        public bool VerifyHashedPassword(Password hashedPassword, Password providedPassword);
    }
}
