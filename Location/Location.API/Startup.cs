using BuildingBlocks.API.ServicesExtensions;
using Location.API.Hubs;
using Location.Application;
using Location.Infrastructure.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace Location.API
{
    public class Startup
    {
        private const string _apiTitle = "Location.API";
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
            services.AddSignalR(opt=> 
            {
                opt.ClientTimeoutInterval = TimeSpan.FromSeconds(60);
            });
            services.AddSwaggerWithJwt(_apiTitle, _apiVersion);
            services.AddJwtAuthentication(Configuration);
            services.AddInfrastructure(Configuration);
            services.AddCors("https://localhost:4200", "http://localhost:4200");
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

            app.UseCors();

            app.UseInfrastructure();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<LocationsHub>("/locations");
                endpoints.UseHangfireEndpoints();
            });
        }
    }
}
