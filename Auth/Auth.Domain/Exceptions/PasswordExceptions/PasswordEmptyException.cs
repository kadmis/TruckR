using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Domain.Exceptions.PasswordExceptions
{
    internal class PasswordEmptyException : Exception
    {
        public PasswordEmptyException():base("Password cannot be empty.")
        {

        }
    }
}
