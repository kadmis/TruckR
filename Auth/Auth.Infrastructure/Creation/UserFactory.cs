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
            return new User(id, new UserName(username), new UserPassword(password), new UserEmail(email));
        }

        public User Create(string username, string password, string email)
        {
            return new User(new UserName(username), new UserPassword(password), new UserEmail(email));
        }
    }
}
