using BuildingBlocks.Application.Data;
using BuildingBlocks.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddSqlConnectionFactory(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton(new ConnectionString(connectionString));
            services.AddTransient<ISqlConnectionFactory, SqlConnectionFactory>();
        }
    }
}
