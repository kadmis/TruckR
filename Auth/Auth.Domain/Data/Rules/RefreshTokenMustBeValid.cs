using Auth.Domain.Data.Entities;
using BuildingBlocks.Domain;
using System;

namespace Auth.Domain.Data.Rules
{
    public class RefreshTokenMustBeValid : IBusinessRule
    {
        private readonly Guid _providedToken;
        private readonly UserAuthentication _userAuthentication;

        public RefreshTokenMustBeValid(Guid providedToken, UserAuthentication userAuthentication)
        {
            _providedToken = providedToken;
            _userAuthentication = userAuthentication;
        }

        public string Message => "Refresh token is invalid.";

        public bool IsBroken()
        {
            return !_providedToken.Equals(_userAuthentication.RefreshToken);
        }
    }
}
