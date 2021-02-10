using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Domain.Exceptions.UserExceptions
{
    internal class NewEmailSameAsOldException : Exception
    {
        public NewEmailSameAsOldException():base("New email cannot be the same as the previous one.")
        {

        }
    }
}
