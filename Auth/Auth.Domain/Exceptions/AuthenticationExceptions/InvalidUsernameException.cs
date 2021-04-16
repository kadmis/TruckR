using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Domain.Exceptions.AuthenticationExceptions
{
    public class InvalidUsernameException : Exception
    {
        public InvalidUsernameException():base("Invalid username.")
        {

        }
    }
}
