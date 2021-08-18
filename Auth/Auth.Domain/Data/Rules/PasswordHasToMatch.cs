using Auth.Domain.Data.Entities;
using Auth.Domain.Data.ValueObjects;
using Auth.Domain.Specifications.PasswordSpecifications.Interfaces;
using BuildingBlocks.Domain;
using System;

namespace Auth.Domain.Data.Rules
{
    public class PasswordHasToMatch : IBusinessRule
    {
        private readonly User _user;
        private readonly Password _givenPassword;
        private readonly IPasswordMatches _passwordMatches;

        public PasswordHasToMatch(User user, Password givenPassword, IPasswordMatches passwordMatches)
        {
            _user = user;
            _givenPassword = givenPassword;
            _passwordMatches = passwordMatches;
        }

        public string Message => "Invalid password";

        public bool IsBroken()
        {
            return !_passwordMatches
                .Setup(_user.Password)
                .IsSatisfiedBy(_givenPassword);
        }
    }
}
