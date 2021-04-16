using Auth.Domain.Exceptions.EmailExceptions;
using BuildingBlocks.Domain;
using System;
using System.Text.RegularExpressions;

namespace Auth.Domain.Data.ValueObjects
{
    public class Email : ValueObject
    {
        private const string _validFormat = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
        public string Value { get; }

        public Email(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new EmailEmptyException();
            }
            if (!Regex.IsMatch(email, _validFormat))
            {
                throw new EmailInvalidFormatException();
            }

            Value = email;
        }
        public Email(Email email):this(email.Value) { }
        private Email() { }
    }
}
