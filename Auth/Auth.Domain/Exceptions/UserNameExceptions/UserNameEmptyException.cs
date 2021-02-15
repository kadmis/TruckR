using System;

namespace Auth.Domain.Exceptions.UsernameExceptions
{
    internal class UsernameEmptyException : Exception
    {
        public UsernameEmptyException() : base("Username cannot be empty.")
        {
        }
    }
}
