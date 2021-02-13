using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Domain.Exceptions.UserExceptions
{
    public class UserInactiveException : Exception
    {
        public UserInactiveException() : base("User is inactive.")
        {
        }
    }
}
