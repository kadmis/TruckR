using Microsoft.Extensions.Configuration;
using System.IO;

namespace Auth.API.Configuration
{
    public class ConfigurationAccessor
    {
        public static IConfiguration Configuration { get; }

        static ConfigurationAccessor()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }
    }
}
