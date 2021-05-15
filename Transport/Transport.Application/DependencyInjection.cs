using BuildingBlocks.Application;
using BuildingBlocks.Application.Identity;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Transport.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediator();
            services.AddIdentityAccessor();
        }

        private static void AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
