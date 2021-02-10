using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Domain.Exceptions.UserNameExceptions
{
    internal class UserNameInvalidFormatException : Exception
    {
        public UserNameInvalidFormatException():base("Invalid username format.")
        {
        }
    }
}
