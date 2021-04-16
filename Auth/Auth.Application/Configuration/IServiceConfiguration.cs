namespace Auth.Application.Configuration
{
    public interface IServiceConfiguration
    {
        public string EncryptionKey { get; }
        public string EncryptionIV { get; }
        public string JwtSecret { get; }
        public string JwtAudience { get; }
        public string JwtIssuer { get; }
        public int JwtExpireInMinutes { get; }
    }
}
