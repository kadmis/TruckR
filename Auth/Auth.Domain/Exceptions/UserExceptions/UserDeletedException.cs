using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Domain.Exceptions.UserExceptions
{
    public class UserDeletedException : Exception
    {
        public UserDeletedException():base("User is  deleted.")
        {
        }
    }
}
