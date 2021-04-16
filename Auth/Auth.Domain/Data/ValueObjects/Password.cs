using Auth.Domain.Exceptions.PasswordExceptions;
using BuildingBlocks.Domain;
using Fare;
using System;
using System.Text.RegularExpressions;

namespace Auth.Domain.Data.ValueObjects
{
    public class Password : ValueObject
    {
        private const int _minLength = 8;
        private const string _validFormat = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$";

        public string Value { get; }

        public Password(string password)
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

            Value = password;
        }
        private Password() { }

        /// <summary>
        /// Generates new valid random password.
        /// </summary>
        /// <returns></returns>
        public static Password Randomize()
        {
            try
            {
                return new Password(new Xeger(_validFormat).Generate());
            }
            catch(Exception)
            {
                throw new PasswordGenerationException();
            }
        }
    }
}
