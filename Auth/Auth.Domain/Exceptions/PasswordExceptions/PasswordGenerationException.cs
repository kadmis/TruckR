using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Domain.Exceptions.PasswordExceptions
{
    public class PasswordGenerationException : Exception
    {
        public PasswordGenerationException() : base("Something went wrong when trying to generate new password.")
        {
        }
    }
}
