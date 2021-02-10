using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Domain.Exceptions.PasswordExceptions
{
    internal class PasswordInvalidFormatException : Exception
    {
        public PasswordInvalidFormatException():base("Invalid password format.")
        {
        }
    }
}
