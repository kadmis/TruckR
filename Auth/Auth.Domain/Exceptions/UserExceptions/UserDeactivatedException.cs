using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Domain.Exceptions.UserExceptions
{
    public class UserDeactivatedException : Exception
    {
        public UserDeactivatedException() : base("User is inactive.")
        {
        }
    }
}
