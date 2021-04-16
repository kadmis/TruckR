using Auth.Domain.Data.Entities;

namespace Auth.Application.JWT
{
    public interface ITokenGenerator
    {
        public string GenerateFor(User user);
    }
}
