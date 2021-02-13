using Auth.Infrastructure.Security.Encryption;
using Xunit;

namespace Auth.Tests
{
    public class EncryptionTests
    {
        [Fact]
        public void Test_ShouldEncrypt_Then_Decrypt()
        {
            var dataString = @"Test_String+?!asxZ5%1\123&^4%rfw1@";
            var encryptor = new Encryptor();

            var encrypted = encryptor.Encrypt(dataString);
            var decrypted = encryptor.Decrypt(encrypted);

            Assert.Equal(dataString, decrypted);
        }

        [Fact]
        public void Test_ShouldEncrypt_Then_ReturnSafelyOnDoubleDecrypt()
        {
            var dataString = @"Test_String+?!asxZ5%1\123&^4%rfw1@";
            var encryptor = new Encryptor();

            var encrypted = encryptor.Encrypt(dataString);
            var decrypted = encryptor.Decrypt(encrypted);
            var safeSecondDecryption = encryptor.Decrypt(decrypted);

            Assert.Equal(dataString, decrypted);
            Assert.Equal(decrypted, safeSecondDecryption);
        }
    }
}
