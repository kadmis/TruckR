using BuildingBlocks.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Reflection;
using Transport.Application.DependencyInjection;
using Transport.Application.Files;
using Transport.Domain.Assignments;
using Transport.Domain.Dispatchers;
using Transport.Domain.Drivers;
using Transport.Infrastructure.Persistence;
using Transport.Infrastructure.Persistence.Context;
using Transport.Infrastructure.Persistence.Files;
using Transport.Infrastructure.Persistence.Repositories;
using Transport.Infrastructure.Processing;

namespace Transport.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplication();
            services.AddPersistence(configuration);
            services.AddCreation();
            services.AddSqlConnectionFactory(configuration.GetConnectionString("DEV"));
            services.AddFileStorage();
            services.AddProcessing();
        }

        private static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TransportContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DEV")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IAssignmentsRepository, AssignmentsRepository>();
            services.AddTransient<IDispatchersRepository, DispatchersRepository>();
            services.AddTransient<IDriversRepository, DriversRepository>();
        }

        private static void AddCreation(this IServiceCollection services)
        {
            services.AddTransient<IAssignmentBuilder, AssignmentBuilder>();
        }

        private static void AddFileStorage(this IServiceCollection services)
        {
            var path = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "Persistence/Files");

            var filesConfiguration = new FilesConfiguration(path);

            services.AddSingleton(filesConfiguration);

            services.AddTransient<IFilesStorage, LocalFilesStorage>();
        }

        private static void AddProcessing(this IServiceCollection services)
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkPipelineBehavior<,>));
        }
    }
}
