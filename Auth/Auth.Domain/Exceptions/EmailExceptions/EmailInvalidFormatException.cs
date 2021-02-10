using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Domain.Exceptions.EmailExceptions
{
    internal class EmailInvalidFormatException : Exception
    {
        public EmailInvalidFormatException():base("Invalid email format.")
        {
        }
    }
}
