using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Domain.Exceptions.UserExceptions
{
    internal class UsernameEmailMatchException : Exception
    {
        public UsernameEmailMatchException():base("Username and email cannot match.")
        {
        }
    }
}
