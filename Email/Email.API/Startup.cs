using Auth.IntegrationEvents;
using BuildingBlocks.EventBus.EventualConsistency;
using BuildingBlocks.EventBus.EventualConsistency.Processing.EventMapper;
using BuildingBlocks.EventBus.EventualConsistency.Processing.EventProcessor;
using BuildingBlocks.EventBus.Externals;
using BuildingBlocks.EventBus.Externals.Events.Handling;
using Email.API.Infrastructure;
using Email.API.Infrastructure.Configuration;
using Email.API.Infrastructure.Database;
using Email.API.Infrastructure.Database.Context;
using Email.API.Infrastructure.Events.Handlers;
using Email.API.Infrastructure.Mapping;
using Email.API.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Quartz;
using System.Reflection;

namespace Email.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Email.API", Version = "v1" });
            });

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

                q.ScheduleJob<EmailSendingJob>(trigger =>
                {
                    trigger.WithIdentity("email-sending-trigger")
                    .WithSimpleSchedule(s => { s.WithIntervalInSeconds(15).RepeatForever(); })
                    .WithPriority(2)
                    .StartNow();
                });
            });
            services.AddQuartzHostedService(opt => { opt.WaitForJobsToComplete = true; });
            services.AddDbContext<EmailContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DEV")));
            services.AddEventContext<EmailContext>();
            services.AddEventServices();

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient<IEmailQueueService, EmailQueueService>();
            services.AddTransient<IEmailService, EmailService>();

            services.AddSingleton(new EmailConfiguration(Configuration));

            var eventMapper = new EventToCommandMapper()
                .Register<UsernameReminderRequestedEvent>(new UsernameReminderEventMap())
                .Register<UserRegisteredEvent>(new UserRegisteredEventMap())
                .Register<UserDeletedEvent>(new UserDeletedEventMap())
                .Register<UserResetPasswordEvent>(new UserResetPasswordEventMap());

            services.AddSingleton(eventMapper);

            services.AddRabbit();
            services.AddTransient<IEventHandler<UserRegisteredEvent>, UserRegisteredEventHandler>();
            services.AddTransient<IEventHandler<UserResetPasswordEvent>, UserResetPasswordEventHandler>();
            services.AddTransient<IEventHandler<UsernameReminderRequestedEvent>, UsernameReminderRequestedEventHandler>();
            services.AddTransient<IEventHandler<UserDeletedEvent>, UserDeletedEventHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Email.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app
                .AddHandler<UserRegisteredEvent>()
                .AddHandler<UserResetPasswordEvent>()
                .AddHandler<UsernameReminderRequestedEvent>()
                .AddHandler<UserDeletedEvent>();
        }
    }
}
