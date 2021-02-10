using Microsoft.Extensions.Configuration;

namespace Email.API.Infrastructure.Configuration
{
    public class EmailConfiguration
    {
        public string Server { get; }
        public int Port { get; }
        public string Name { get; }
        public string Email { get; }
        public string Password { get; }

        public EmailConfiguration(IConfiguration configuration)
        {
            Server = configuration["Email:Server"];
            Port = int.Parse(configuration["Email:Port"]);
            Name = configuration["Email:Name"];
            Email = configuration["Email:Login"];
            Password = configuration["Email:Password"];
        }
    }
}
