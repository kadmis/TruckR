using Auth.Domain.Creation;
using Auth.Domain.Data.Entities;
using Auth.Domain.Data.ValueObjects;
using System;

namespace Auth.Infrastructure.Creation
{
    public class UserFactory : IUserFactory
    {
        public UserFactory()
        {
        }

        public User Create(Guid id, string username, string password, string email)
        {
            return new User(id, new Username(username), new Password(password), new Email(email));
        }

        public User Create(string username, string password, string email)
        {
            return new User(new Username(username), new Password(password), new Email(email));
        }
    }
}
