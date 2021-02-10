using Auth.Domain.Exceptions.PasswordExceptions;
using Fare;
using System;
using System.Text.RegularExpressions;

namespace Auth.Domain.Data.ValueObjects
{
    public class UserPassword
    {
        private const int _minLength = 8;
        private const string _validFormat = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$";

        public string Password { get; }

        public UserPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new PasswordEmptyException();
            }
            if(password.Length < _minLength)
            {
                throw new PasswordInvalidLengthException(_minLength);
            }
            if (!Regex.IsMatch(password, _validFormat))
            {
                throw new PasswordInvalidFormatException();
            }

            Password = password;
        }
        private UserPassword() { }

        /// <summary>
        /// Generates new valid random password.
        /// </summary>
        /// <returns></returns>
        public static UserPassword Randomize()
        {
            try
            {
                return new UserPassword(new Xeger(_validFormat).Generate());
            }
            catch(Exception)
            {
                throw new PasswordGenerationException();
            }
        }
    }
}
