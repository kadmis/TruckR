using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Domain.Exceptions.UserExceptions
{
    internal class InvalidActivationIdException : Exception
    {
        public InvalidActivationIdException():base("Provided activation ID is invalid.")
        {
        }
    }
}
