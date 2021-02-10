using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Domain.Exceptions.UserExceptions
{
    internal class NewUsernameSameAsOldException : Exception
    {
        public NewUsernameSameAsOldException():base("New username cannot be the same as the previous one.")
        {

        }
    }
}
