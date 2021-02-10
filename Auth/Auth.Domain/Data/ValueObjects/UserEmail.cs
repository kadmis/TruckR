using Auth.Domain.Exceptions;
using Auth.Domain.Exceptions.EmailExceptions;
using System;
using System.Text.RegularExpressions;

namespace Auth.Domain.Data.ValueObjects
{
    public class UserEmail : IValueObject
    {
        private const string _validFormat = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
        public string Email { get; }

        public UserEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new EmailEmptyException();
            }
            if (!Regex.IsMatch(email, _validFormat))
            {
                throw new EmailInvalidFormatException();
            }

            Email = email;
        }
        public UserEmail(UserEmail email):this(email.Email) { }
        private UserEmail() { }

        public bool Equals(UserEmail email)
        {
            return Email.ToUpper().Equals(email.Email.ToUpper());
        }

        public override bool Equals(object obj)
        {
            return Email.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Email.GetHashCode();
        }

        public override string ToString()
        {
            return Email;
        }

        public static bool operator ==(UserEmail email1, UserEmail email2)
        {
            return email1.Email.ToUpper().Equals(email2.Email.ToUpper());
        }

        public static bool operator !=(UserEmail email1, UserEmail email2)
        {
            return !email1.Email.ToUpper().Equals(email2.Email.ToUpper());
        }
    }
}
