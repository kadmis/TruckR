using BuildingBlocks.Domain;
using System;

namespace Auth.Domain.Data.ValueObjects
{
    public class Token : ValueObject
    {
        public string Value { get; private set; }
        public DateTime ValidUntil { get; private set; }

        public Token(string value, DateTime validUntil)
        {
            Value = value;
            ValidUntil = validUntil;
        }
    }
}
