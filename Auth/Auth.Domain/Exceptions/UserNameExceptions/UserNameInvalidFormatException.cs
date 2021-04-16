using System;

namespace Auth.Domain.Exceptions.UsernameExceptions
{
    public class UsernameInvalidFormatException : Exception
    {
        public UsernameInvalidFormatException() : base("Invalid username format.")
        {
        }
    }
}
