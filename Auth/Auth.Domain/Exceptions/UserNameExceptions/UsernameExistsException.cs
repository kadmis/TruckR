using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Domain.Exceptions.UserNameExceptions
{
    internal class UsernameExistsException : Exception
    {
        public UsernameExistsException() : base("Username already exists.")
        {

        }
    }
}
