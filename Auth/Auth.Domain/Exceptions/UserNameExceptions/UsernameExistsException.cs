using System;

namespace Auth.Domain.Exceptions.UsernameExceptions
{
    public class UsernameExistsException : Exception
    {
        public UsernameExistsException() : base("Username already exists.")
        {

        }
    }
}
