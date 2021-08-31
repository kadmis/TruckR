using Auth.Domain.Data.Rules;
using Auth.Domain.Data.ValueObjects;
using Auth.Domain.Specifications.PasswordSpecifications.Interfaces;
using BuildingBlocks.Domain;
using System;

namespace Auth.Domain.Data.Entities
{
    public class UserAuthentication : Entity, IAggregateRoot
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public DateTime AuthenticationDate { get; private set; }
        public DateTime ValidUntil { get; private set; }
        public Guid? RefreshToken { get; private set; }

        private UserAuthentication()
        {
        }

        public static UserAuthentication Authenticate(User user, Password givenPassword, IPasswordMatches passwordMatches)
        {
            CheckRule(new UserHasToExist(user));
            CheckRule(new PasswordHasToMatch(user, givenPassword, passwordMatches));
            CheckRule(new UserCannotBeInDeletedState(user));
            CheckRule(new UserCannotBeInInactiveState(user));

            return new UserAuthentication
            {
                UserId = user.Id,
                Id = Guid.NewGuid()
            };
        }

        public static UserAuthentication Refresh(User user, UserAuthentication userAuthentication, Guid providedToken)
        {
            CheckRule(new UserHasToExist(user));
            CheckRule(new UserCannotBeInDeletedState(user));
            CheckRule(new UserCannotBeInInactiveState(user));
            CheckRule(new UserAuthenticationMustBeValid(userAuthentication));
            CheckRule(new RefreshTokenMustBeValid(providedToken, userAuthentication));

            if(userAuthentication.ValidUntil >= Clock.Now)
            {
                userAuthentication.GenerateRefreshToken();
                return userAuthentication;
            }

            userAuthentication.Invalidate();

            return new UserAuthentication
            {
                UserId = user.Id,
                Id = Guid.NewGuid()
            };
        }

        public UserAuthentication Activate(DateTime tokenValidUntil)
        {
            AuthenticationDate = Clock.Now;
            ValidUntil = tokenValidUntil.AddDays(7);
            RefreshToken = Guid.NewGuid();

            return this;
        }

        public void Invalidate()
        {
            ValidUntil = Clock.Now.AddMinutes(-1);
            RefreshToken = null;
        }

        private void GenerateRefreshToken()
        {
            RefreshToken = Guid.NewGuid();
        }
    }
}
