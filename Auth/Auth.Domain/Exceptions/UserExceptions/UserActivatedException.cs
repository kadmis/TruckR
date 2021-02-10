using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Domain.Exceptions.UserExceptions
{
    public class UserActivatedException : Exception
    {
        public UserActivatedException():base("User is active.")
        {

        }
    }
}
