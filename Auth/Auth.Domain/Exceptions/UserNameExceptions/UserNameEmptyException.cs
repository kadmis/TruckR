using System;

namespace Auth.Domain.Exceptions.UsernameExceptions
{
    public class UsernameEmptyException : Exception
    {
        public UsernameEmptyException() : base("Username cannot be empty.")
        {
        }
    }
}
