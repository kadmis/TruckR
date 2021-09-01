using Auth.IntegrationEvents;
using BuildingBlocks.EventBus.Externals;
using BuildingBlocks.EventBus.Externals.Events.Handling;
using BuildingBlocks.EventBus.Externals.EventualConsistency;
using BuildingBlocks.EventBus.Externals.EventualConsistency.Processing.EventMapper;
using BuildingBlocks.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System.IO;
using System.Reflection;
using TaskManagement.Infrastructure.Configuration.Processing;
using Transport.Application.DependencyInjection;
using Transport.Application.Files;
using Transport.Domain.Assignments;
using Transport.Domain.Groups;
using Transport.Infrastructure.Events.Handlers;
using Transport.Infrastructure.Events.Mapping;
using Transport.Infrastructure.Events.Processing;
using Transport.Infrastructure.Jobs;
using Transport.Infrastructure.Persistence;
using Transport.Infrastructure.Persistence.Context;
using Transport.Infrastructure.Persistence.Files;
using Transport.Infrastructure.Persistence.Repositories;
using Transport.Infrastructure.Processing;
using Transport.SignalRClient;

namespace Transport.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistence(configuration);
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddApplication();
            services.AddProcessing();
            services.AddCreation();
            services.AddSqlConnectionFactory(configuration.GetConnectionString("DEV"));
            services.AddFileStorage();
            services.AddEvents();
            services.AddHubClients("https://localhost:44318");
            services.AddAssignmentsProcessing();
        }

        private static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TransportContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DEV")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IAssignmentsRepository, AssignmentsRepository>();
            services.AddTransient<ITransportGroupsRepository, TransportGroupsRepository>();
        }

        private static void AddCreation(this IServiceCollection services)
        {
            services.AddTransient<IAssignmentBuilder, AssignmentBuilder>();
        }

        private static void AddFileStorage(this IServiceCollection services)
        {
            var path = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "");

            var filesConfiguration = new LocalFilesConfiguration(path);

            services.AddSingleton(filesConfiguration);

            services.AddTransient<IFilesStorage, LocalFilesStorage>();
        }

        private static void AddProcessing(this IServiceCollection services)
        {
            services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkPipelineBehavior<,>));
        }

        private static void AddEvents(this IServiceCollection services)
        {
            services.AddQuartz(q =>
            {
                q.UseDefaultThreadPool(tp => { tp.MaxConcurrency = 1; });
                q.UseMicrosoftDependencyInjectionJobFactory();

                q.ScheduleJob<EventProcessingJob>(trigger =>
                {
                    trigger.WithIdentity("event-processing-trigger")
                    .WithSimpleSchedule(s => { s.WithIntervalInSeconds(10).RepeatForever(); })
                    .WithPriority(1)
                    .StartNow();
                });
            });
            services.AddQuartzHostedService(opt => { opt.WaitForJobsToComplete = true; });
            services.AddEventContext<TransportContext>();
            services.AddEventServices();

            var eventMapper = new EventToCommandMapper()
                .Register<DispatcherActivatedEvent>(new DispatcherActivatedEventMap())
                .Register<DriverActivatedEvent>(new DriverActivatedEventMap());

            services.AddSingleton(eventMapper);

            services.AddRabbit();
            services.AddTransient<IEventHandler<DispatcherActivatedEvent>, DispatcherActivatedEventHandler>();
            services.AddTransient<IEventHandler<DriverActivatedEvent>, DriverActivatedEventHandler>();
        }

        private static void AddAssignmentsProcessing(this IServiceCollection services)
        {
            services.AddQuartz(q =>
            {
                q.UseDefaultThreadPool(tp => { tp.MaxConcurrency = 1; });
                q.UseMicrosoftDependencyInjectionJobFactory();

                q.ScheduleJob<AssignmentCheckJob>(trigger =>
                {
                    trigger.WithIdentity("assignment-check-trigger")
                    .WithSimpleSchedule(s => { s.WithIntervalInSeconds(120).RepeatForever(); })
                    .WithPriority(2)
                    .StartNow();
                });
            });
        }
    }
}
