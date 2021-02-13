using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Domain.Exceptions.PasswordExceptions
{
    internal class PasswordGenerationException : Exception
    {
        public PasswordGenerationException() : base("Something went wrong when trying to generate new password.")
        {
        }
    }
}
