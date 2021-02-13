using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Domain.Exceptions.UserExceptions
{
    public class UserActiveException : Exception
    {
        public UserActiveException():base("User is active.")
        {

        }
    }
}
