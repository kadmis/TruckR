namespace Auth.Domain.Security.PersonalData
{
    public interface IEncryptor
    {
        public string Encrypt(string data);
        public string Decrypt(string data);
    }
}
