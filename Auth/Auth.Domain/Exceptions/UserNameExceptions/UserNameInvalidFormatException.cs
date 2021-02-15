using System;

namespace Auth.Domain.Exceptions.UsernameExceptions
{
    internal class UsernameInvalidFormatException : Exception
    {
        public UsernameInvalidFormatException() : base("Invalid username format.")
        {
        }
    }
}
