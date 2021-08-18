﻿using Auth.IntegrationEvents;
using BuildingBlocks.EventBus.EventualConsistency;
using BuildingBlocks.EventBus.EventualConsistency.Processing.EventMapper;
using BuildingBlocks.EventBus.Externals;
using BuildingBlocks.EventBus.Externals.Events.Handling;
using BuildingBlocks.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System.IO;
using System.Reflection;
using Transport.Application.DependencyInjection;
using Transport.Application.Files;
using Transport.Domain.Assignments;
using Transport.Domain.Groups;
using Transport.Infrastructure.Events.Handlers;
using Transport.Infrastructure.Events.Mapping;
using Transport.Infrastructure.Events.Processing;
using Transport.Infrastructure.Persistence;
using Transport.Infrastructure.Persistence.Context;
using Transport.Infrastructure.Persistence.Files;
using Transport.Infrastructure.Persistence.Repositories;

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
            services.AddEvents();
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
                "Persistence/Files");

            var filesConfiguration = new LocalFilesConfiguration(path);

            services.AddSingleton(filesConfiguration);

            services.AddTransient<IFilesStorage, LocalFilesStorage>();
        }

        private static void AddEvents(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
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
    }
}
