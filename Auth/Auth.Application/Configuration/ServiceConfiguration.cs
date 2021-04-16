using Microsoft.Extensions.Configuration;
using System;

namespace Auth.Application.Configuration
{
    public class ServiceConfiguration : IServiceConfiguration
    {
        public string EncryptionKey { get; }
        public string EncryptionIV { get; }
        public string JwtSecret { get; }
        public string JwtAudience { get; }
        public string JwtIssuer { get; }
        public int JwtExpireInMinutes { get; }

        public ServiceConfiguration(IConfiguration configuration)
        {
            JwtSecret = configuration["Jwt:Secret"];
            JwtAudience = configuration["Jwt:ValidAudience"];
            JwtIssuer = configuration["Jwt:ValidIssuer"];
            JwtExpireInMinutes = Convert.ToInt32(configuration["Jwt:ExpireInMinutes"]);
            EncryptionKey = configuration["Encryption:Key"];
            EncryptionIV = configuration["Encryption:IV"];
        }
    }
}
