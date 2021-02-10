using Auth.Domain.Security.Passwords;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Auth.Infrastructure.Security.Passwords
{
    public class HashedPasswordConverter : ValueConverter<string, string>
    {
        public HashedPasswordConverter(IPasswordHasher hasher, ConverterMappingHints mappingHints = default) : base(x=>hasher.HashPassword(x), x=>x, mappingHints)
        {
        }
    }
}
