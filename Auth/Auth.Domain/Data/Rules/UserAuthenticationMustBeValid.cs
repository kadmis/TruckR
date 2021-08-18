using Auth.Domain.Data.Entities;
using BuildingBlocks.Domain;

namespace Auth.Domain.Data.Rules
{
    public class UserAuthenticationMustBeValid : IBusinessRule
    {
        private readonly UserAuthentication _userAuthentication;

        public UserAuthenticationMustBeValid(UserAuthentication userAuthentication)
        {
            _userAuthentication = userAuthentication;
        }

        public string Message => "Invalid authentication id.";

        public bool IsBroken()
        {
            return _userAuthentication == null || !_userAuthentication.RefreshToken.HasValue;
        }
    }
}
