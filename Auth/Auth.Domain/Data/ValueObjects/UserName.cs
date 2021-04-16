using Auth.Domain.Exceptions.UsernameExceptions;
using BuildingBlocks.Domain;
using System;
using System.Text.RegularExpressions;

namespace Auth.Domain.Data.ValueObjects
{
    public class Username : ValueObject
    {
        private const string _validFormat = @"^(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$";
        private const int _minLength = 8;
        private const int _maxLength = 25;
        public string Value { get; }

        public Username(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new UsernameEmptyException();
            }
            if (username.Length < _minLength || username.Length > _maxLength)
            {
                throw new UsernameInvalidLengthException(_minLength, _maxLength);
            }
            if (!Regex.IsMatch(username, _validFormat))
            {
                throw new UsernameInvalidFormatException();
            }

            Value = username;
        }
        public Username(Username username) : this(username.Value) { }
        private Username() { }
    }
}
