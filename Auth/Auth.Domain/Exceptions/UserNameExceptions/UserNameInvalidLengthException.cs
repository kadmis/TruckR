using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Domain.Exceptions.UserNameExceptions
{
    internal class UserNameInvalidLengthException : Exception
    {
        public UserNameInvalidLengthException(int minLength, int maxLength):base($"Username has to be between {minLength} and {maxLength} characters long.")
        {

        }
    }
}
