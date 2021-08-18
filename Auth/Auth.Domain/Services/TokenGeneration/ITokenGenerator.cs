using Auth.Domain.Data.Entities;
using Auth.Domain.Data.ValueObjects;

namespace Auth.Domain.Services.TokenGeneration
{
    public interface ITokenGenerator
    {
        public Token GenerateFor(User user, UserAuthentication userAuthentication);
    }
}
