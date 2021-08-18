using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;
using Location.Application;
using Location.Application.DTO;
using Location.Domain.Repositories;
using Location.Domain.Services;
using Location.Domain.ValueObjects;
using Location.Infrastructure.Cache;
using Location.Infrastructure.Cache.Interfaces;
using Location.Infrastructure.Database;
using Location.Infrastructure.Database.MongoMappers;
using Location.Infrastructure.Jobs.Recurring;
using Location.Infrastructure.Services;
using Location.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using StackExchange.Redis;

namespace Location.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRedisCache(configuration);
            services.AddMongoDB(configuration);
            services.AddServices();
            services.AddTemporaryStorageServices();
            services.AddHangfire(configuration);
            services.AddApplication();
        }

        public static void UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseHangfire();
            RegisterRecurringJobs();
        }

        private static void AddRedisCache(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConnectionMultiplexer>(x => ConnectionMultiplexer.Connect(configuration["Cache:Redis:ConnectionString"]));
            services.AddTransient<ILocationCacheService, RedisCacheService>();
        }

        private static void AddMongoDB(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMongoClient>(new MongoClient(configuration["Database:MongoDB:ConnectionString"]));
            services.AddScoped<IStatisticsRepository, StatisticMongoRepository>();
            services.AddTransient<IStatisticsReadOnlyRepository, StatisticsMongoReadOnlyRepository>();
            services.AddSingleton<LocationMongoContext>();
            StatisticMongoMap.Map();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IStatisticsPersistingService, StatisticsPersistingService>();
            services.AddSingleton<ILocationsToStatisticsConverter, LocationsToStatisticsConverter>();
            services.AddSingleton<IDistanceCalculator>(new VincentyDistanceCalculator(Ellipsoid.WGS84));
        }

        private static void AddTemporaryStorageServices(this IServiceCollection services)
        {
            services.AddTransient<ITemporaryStorageService<LocationModel>, CachedTemporaryStorageService>();
        }

        private static void AddHangfire(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoConnection = configuration["Database:MongoDB:ConnectionString"];

            var mongoUrlBuilder = new MongoUrlBuilder($"{mongoConnection}/jobs");
            var mongoClient = new MongoClient(mongoUrlBuilder.ToMongoUrl());

            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseMongoStorage(mongoClient, mongoUrlBuilder.DatabaseName, new MongoStorageOptions
                {
                    MigrationOptions = new MongoMigrationOptions
                    {
                        MigrationStrategy = new MigrateMongoMigrationStrategy(),
                        BackupStrategy = new CollectionMongoBackupStrategy()
                    },
                    Prefix = "hangfire.mongo",
                    CheckConnection = true,
                })
            );
            // Add the processing server as IHostedService
            services.AddHangfireServer(serverOptions =>
            {
                serverOptions.ServerName = "Hangfire.Mongo.Locations";
            });
        }

        private static void RegisterRecurringJobs()
        {
            PersistStatisticsJob.Register();
        }

        private static void UseHangfire(this IApplicationBuilder app)
        {
            app.UseHangfireDashboard();
        }

        public static void UseHangfireEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHangfireDashboard();
        }
    }
}
