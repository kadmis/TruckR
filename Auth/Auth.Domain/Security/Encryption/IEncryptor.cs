namespace Auth.Domain.Security.Encryption
{
    public interface IEncryptor
    {
        public string Encrypt(string data);
        public string Decrypt(string data);
    }
}
