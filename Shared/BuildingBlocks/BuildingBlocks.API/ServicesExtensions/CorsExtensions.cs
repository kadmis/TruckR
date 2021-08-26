using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.API.ServicesExtensions
{
    public static class CorsExtensions
    {
        public static void AddCors(this IServiceCollection services, params string[] origins)
        {
            services.AddCors(opt =>
            {
                opt.AddDefaultPolicy(builder =>
                    builder
                        .SetIsOriginAllowed(_ => true)
                        .WithOrigins(origins)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
        }
    }
}
