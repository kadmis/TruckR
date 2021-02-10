using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Domain.Exceptions.AuthenticationExceptions
{
    internal class InvalidUsernameException : Exception
    {
        public InvalidUsernameException():base("Invalid username.")
        {

        }
    }
}
