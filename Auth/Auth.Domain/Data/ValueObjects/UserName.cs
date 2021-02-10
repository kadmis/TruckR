using Auth.Domain.Exceptions.UserNameExceptions;
using System;
using System.Text.RegularExpressions;

namespace Auth.Domain.Data.ValueObjects
{
    public class UserName : IValueObject
    {
        private const string _validFormat = @"^(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$";
        private const int _minLength = 8;
        private const int _maxLength = 20;
        public string Username { get; }

        public UserName(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new UserNameEmptyException();
            }
            if (username.Length < _minLength || username.Length > _maxLength)
            {
                throw new UserNameInvalidLengthException(_minLength, _maxLength);
            }
            if (!Regex.IsMatch(username, _validFormat))
            {
                throw new UserNameInvalidFormatException();
            }

            Username = username;
        }
        public UserName(UserName username) : this(username.Username) { }
        private UserName() { }

        public bool Equals(UserName username)
        {
            return Username.ToUpper().Equals(username.Username.ToUpper());
        }

        public override bool Equals(object obj)
        {
            return Username.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Username.GetHashCode();
        }

        public override string ToString()
        {
            return Username;
        }

        public static bool operator ==(UserName username1, UserName username2)
        {
            return username1.Username.ToUpper().Equals(username2.Username.ToUpper());
        }

        public static bool operator !=(UserName username1, UserName username2)
        {
            return !username1.Username.ToUpper().Equals(username2.Username.ToUpper());
        }
    }
}
