using Auth.Domain.Data.Entities;
using BuildingBlocks.Domain;
using System;

namespace Auth.Domain.Data.Rules
{
    public class PasswordResetTokenHasToBeValid : IBusinessRule
    {
        private readonly Guid _givenResetToken;
        private readonly User _user;

        public PasswordResetTokenHasToBeValid(Guid givenResetToken, User user)
        {
            _givenResetToken = givenResetToken;
            _user = user;
        }

        public string Message => "Password reset token is invalid or password reset has not been requested.";

        public bool IsBroken()
        {
            return _user.PasswordResetToken == null || !_givenResetToken.Equals(_user.PasswordResetToken);
        }
    }
}
