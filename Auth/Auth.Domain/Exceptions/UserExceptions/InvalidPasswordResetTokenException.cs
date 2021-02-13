using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.Exceptions.UserExceptions
{
    internal class InvalidPasswordResetTokenException : Exception
    {
        public InvalidPasswordResetTokenException():base("Password reset token is invalid or password reset was not requested.")
        {
        }
    }
}
