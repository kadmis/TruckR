using Auth.IntegrationEvents;
using Email.API.Infrastructure.Configuration;
using Email.API.Infrastructure.Messaging;
using Email.API.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SharedRabbitMQ;
using SharedRabbitMQ.Externals.Events.Handling;

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
            services.AddTransient<IEmailService, EmailService>();
            services.AddSingleton(new EmailConfiguration(Configuration));
            services.AddRabbit();
            services.AddTransient<IEventHandler<UserRegisteredEvent>, UserRegisteredEventHandler>();
            services.AddTransient<IEventHandler<UserResetPasswordEvent>, UserResetPasswordEventHandler>();
            services.AddTransient<IEventHandler<UsernameReminderRequestedEvent>, UsernameReminderRequestedEventHandler>();
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
                .AddHandler<UsernameReminderRequestedEvent>();
        }
    }
}
