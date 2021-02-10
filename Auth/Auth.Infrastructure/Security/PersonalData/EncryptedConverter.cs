using Auth.Domain.Security.PersonalData;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Auth.Infrastructure.Security.PersonalData
{
    public class EncryptedConverter : ValueConverter<string, string>
    {
        public EncryptedConverter(IEncryptor encryptor, ConverterMappingHints mappingHints = default) : base(x=>encryptor.Encrypt(x), x=>encryptor.Decrypt(x), mappingHints)
        {
        }
    }
}
