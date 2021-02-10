using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Domain.Exceptions
{
    public class EmailExistsException : Exception
    {
        public EmailExistsException() : base("Email already exists.")
        {

        }
    }
}
