using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Domain.Exceptions.PasswordExceptions
{
    public class PasswordInvalidFormatException : Exception
    {
        public PasswordInvalidFormatException():base("Invalid password format.")
        {
        }
    }
}
