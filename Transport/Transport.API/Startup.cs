using Auth.IntegrationEvents;
using BuildingBlocks.API.ServicesExtensions;
using BuildingBlocks.EventBus.Externals;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using Transport.Infrastructure.DependencyInjection;

namespace Transport.API
{
    public class Startup
    {
        private const string _apiTitle = "Transport.API";
        private const string _apiVersion = "v1";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerWithJwt(_apiTitle, _apiVersion);
            services.AddJwtAuthentication(Configuration);
            services.AddInfrastructure(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint($"/swagger/{_apiVersion}/swagger.json", $"{_apiTitle} {_apiVersion}"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app
                .AddHandler<DispatcherActivatedEvent>()
                .AddHandler<DriverActivatedEvent>();
        }
    }
}
