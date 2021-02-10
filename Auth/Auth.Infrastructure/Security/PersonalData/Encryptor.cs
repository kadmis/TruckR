using Auth.Domain.Security.PersonalData;
using Auth.Infrastructure.Configuration;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Auth.Infrastructure.Security.PersonalData
{
    public class Encryptor : IEncryptor
    {
        private readonly SymmetricAlgorithm _algorithm;

        public Encryptor(IServiceConfiguration configuration)
        {
            _algorithm = Aes.Create();
            _algorithm.Key = SetKey(configuration.EncryptionKey);
            _algorithm.IV = SetIV(configuration.EncryptionIV);
        }
        public Encryptor(string key, string iv)
        {
            _algorithm = Aes.Create();
            _algorithm.Key = SetKey(key);
            _algorithm.IV = SetIV(iv);
        }
        public Encryptor()
        {
            _algorithm = Aes.Create();
            _algorithm.GenerateKey();
            _algorithm.GenerateIV();
        }

        public string Decrypt(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                return data;
            }

            try
            {
                var bytes = Convert.FromBase64String(data);
                var decryptor = _algorithm.CreateDecryptor();

                using var memStream = new MemoryStream(bytes);
                using var cryptoStream = new CryptoStream(memStream, decryptor, CryptoStreamMode.Read);
                using var reader = new StreamReader(cryptoStream, Encoding.UTF8);

                return reader.ReadToEnd();
            }
            catch (Exception)
            {
                return data;
            }
        }

        public string Encrypt(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                return data;
            }

            try
            {
                var bytes = Encoding.UTF8.GetBytes(data);
                var encryptor = _algorithm.CreateEncryptor();
                byte[] encryptedBytes;

                using (var memStream = new MemoryStream())
                {
                    using var cryptoStream = new CryptoStream(memStream, encryptor, CryptoStreamMode.Write);
                    using (var writer = new BinaryWriter(cryptoStream, Encoding.UTF8))
                    {
                        writer.Write(bytes);
                    }
                    encryptedBytes = memStream.ToArray();
                }

                return Convert.ToBase64String(encryptedBytes);
            }
            catch (Exception)
            {
                return data;
            }
        }

        private byte[] SetKey(string key)
        {
            return string.IsNullOrWhiteSpace(key) ? throw new Exception("Key cannot be empty.") : Convert.FromBase64String(key);
        }
        private byte[] SetIV(string iv)
        {
            return string.IsNullOrWhiteSpace(iv) ? throw new Exception("IV cannot be empty.") : Convert.FromBase64String(iv);
        }
    }
}
