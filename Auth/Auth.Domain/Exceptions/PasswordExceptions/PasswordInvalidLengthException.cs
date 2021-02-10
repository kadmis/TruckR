using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Domain.Exceptions.PasswordExceptions
{
    internal class PasswordInvalidLengthException : Exception
    {
        public PasswordInvalidLengthException(int minLength):base($"Password has to be at least {minLength} characters long.")
        {

        }
        public PasswordInvalidLengthException(int minLength, int maxLength) : base($"Password has to be between {minLength} and {maxLength} characters long.")
        {

        }
    }
}
