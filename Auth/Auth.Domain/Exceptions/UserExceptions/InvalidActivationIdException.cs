using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Domain.Exceptions.UserExceptions
{
    public class InvalidActivationIdException : Exception
    {
        public InvalidActivationIdException():base("Provided activation ID is invalid.")
        {
        }
    }
}
