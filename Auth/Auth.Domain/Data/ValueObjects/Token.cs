using BuildingBlocks.Domain;
using System;

namespace Auth.Domain.Data.ValueObjects
{
    public class Token : ValueObject
    {
        public string Value { get; private set; }
        public DateTime ValidUntil { get; private set; }
        public long ExpiryInMiliseconds { get; private set; }

        public Token(string value, DateTime validUntil, long expiryInMiliseconds)
        {
            Value = value;
            ValidUntil = validUntil;
            ExpiryInMiliseconds = expiryInMiliseconds;
        }

        public long RefreshInterval => ExpiryInMiliseconds - 30_000;
    }
}
