using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Transport.Infrastructure.Persistence;
using Transport.Infrastructure.Persistence.Context;

namespace Transport.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistence(configuration);
        }

        private static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TransportContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DEV")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
