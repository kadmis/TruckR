using BuildingBlocks.Application.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BuildingBlocks.Application
{
    public static class DependencyInjection
    {
        public static void AddIdentityAccessor(this IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IIdentityAccessor, IdentityAccessor>();
        }
    }
}
