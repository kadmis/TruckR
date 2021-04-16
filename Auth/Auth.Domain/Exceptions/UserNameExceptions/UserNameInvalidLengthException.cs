using System;

namespace Auth.Domain.Exceptions.UsernameExceptions
{
    public class UsernameInvalidLengthException : Exception
    {
        public UsernameInvalidLengthException(int minLength, int maxLength) : base($"Username has to be between {minLength} and {maxLength} characters long.")
        {

        }
    }
}
