using Auth.Domain.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Domain.Creation
{
    public interface IUserFactory
    {
        public User Create(Guid id, string username, string password, string email);
        public User Create(string username, string password, string email);
    }
}
