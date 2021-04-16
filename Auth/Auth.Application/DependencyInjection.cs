using Auth.Application.JWT;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Auth.Application
{
    public static class DependencyInjection
    {
        public static void AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }

        public static void AddTokenGenerator(this IServiceCollection services)
        {
            services.AddSingleton<ITokenGenerator, TokenGenerator>();
        }
    }
}
