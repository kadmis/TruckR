using System;

namespace Auth.Domain.Exceptions.UsernameExceptions
{
    internal class UsernameExistsException : Exception
    {
        public UsernameExistsException() : base("Username already exists.")
        {

        }
    }
}
