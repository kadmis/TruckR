using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Domain.Exceptions.UserExceptions
{
    public class UserDoesntExistException : Exception
    {
        public UserDoesntExistException() : base("User doesn't exist.")
        {

        }
    }
}
