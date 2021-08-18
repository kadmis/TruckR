using Auth.Domain.Data.Entities;
using BuildingBlocks.Domain;
using System;

namespace Auth.Domain.Data.Rules
{
    public class ActivationTokenHasToBeValid : IBusinessRule
    {
        private readonly Guid _givenActivationToken;
        private readonly User _user;

        public ActivationTokenHasToBeValid(Guid givenActivationToken, User user)
        {
            _givenActivationToken = givenActivationToken;
            _user = user;
        }

        public string Message => "Activation token is invalid.";

        public bool IsBroken()
        {
            return !_user.ActivationId.Equals(_givenActivationToken);
        }
    }
}
