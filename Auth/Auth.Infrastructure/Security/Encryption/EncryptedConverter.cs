using Auth.Domain.Security.Encryption;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Auth.Infrastructure.Security.Encryption
{
    public class EncryptedConverter : ValueConverter<string, string>
    {
        public EncryptedConverter(IEncryptor encryptor, ConverterMappingHints mappingHints = default) : base(x => encryptor.Encrypt(x), x => encryptor.Decrypt(x), mappingHints)
        {
        }
    }
}
