using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Domain.Exceptions.UserNameExceptions
{
    internal class UserNameEmptyException : Exception
    {
        public UserNameEmptyException():base("Username cannot be empty.")
        {
        }
    }
}
