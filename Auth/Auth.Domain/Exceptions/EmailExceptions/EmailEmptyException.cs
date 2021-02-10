using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Domain.Exceptions.EmailExceptions
{
    internal class EmailEmptyException : Exception
    {
        public EmailEmptyException() : base("Email cannot be empty.")
        {
        }
    }
}
