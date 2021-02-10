using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Domain.Exceptions.AuthenticationExceptions
{
    internal class InvalidPasswordException : Exception
    {
        public InvalidPasswordException():base("Invalid password.")
        {

        }
    }
}
