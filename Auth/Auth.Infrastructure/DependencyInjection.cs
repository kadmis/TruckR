using Auth.Domain.Creation;
using Auth.Domain.Persistence;
using Auth.Domain.Security.Passwords;
using Auth.Domain.Security.PersonalData;
using Auth.Domain.Services.Authentication;
using Auth.Domain.Services.Registration;
using Auth.Domain.Services.UserOperations;
using Auth.Infrastructure.Configuration;
using Auth.Infrastructure.Creation;
using Auth.Infrastructure.Persistence;
using Auth.Infrastructure.Persistence.Context;
using Auth.Infrastructure.Security.Passwords;
using Auth.Infrastructure.Security.PersonalData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistence(configuration);
            services.AddDomainServices();
            services.AddFactories();
            services.AddDataEncryption();
            services.AddServiceConfiguration();
        }

        private static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuthContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DEV")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static void AddDomainServices(this IServiceCollection services)
        {
            services.AddTransient<IUserRegistrationService, UserRegistrationService>();
            services.AddTransient<IUserAuthenticationService, UserAuthenticationService>();
            services.AddTransient<IUserOperationsService, UserOperationsService>();
        }

        private static void AddFactories(this IServiceCollection services)
        {
            services.AddTransient<IUserFactory, UserFactory>();
        }

        private static void AddDataEncryption(this IServiceCollection services)
        {
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<IEncryptor, Encryptor>();
        }

        private static void AddServiceConfiguration(this IServiceCollection services)
        {
            services.AddSingleton<IServiceConfiguration, ServiceConfiguration>();
        }
    }
}
