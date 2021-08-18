using Auth.Application.JWT;
using Auth.Domain.Services.TokenGeneration;
using BuildingBlocks.Application;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Auth.Application
{
    public static class DependencyInjection
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddMediator();
            services.AddTokenGenerator();
            services.AddIdentityAccessor();
        }
        private static void AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }

        private static void AddTokenGenerator(this IServiceCollection services)
        {
            services.AddSingleton<ITokenGenerator, TokenGenerator>();
        }
    }
}
