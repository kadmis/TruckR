using Auth.Application;
using Auth.Application.Configuration;
using Auth.Domain.Persistence;
using Auth.Domain.Security.Encryption;
using Auth.Domain.Security.Passwords;
using Auth.Domain.Services.Authentication;
using Auth.Domain.Services.Registration;
using Auth.Domain.Specifications.EmailSpecifications;
using Auth.Domain.Specifications.EmailSpecifications.Interfaces;
using Auth.Domain.Specifications.PasswordSpecifications;
using Auth.Domain.Specifications.PasswordSpecifications.Interfaces;
using Auth.Domain.Specifications.UsernameSpecifications;
using Auth.Domain.Specifications.UsernameSpecifications.Interfaces;
using Auth.Infrastructure.Messaging;
using Auth.Infrastructure.Persistence;
using Auth.Infrastructure.Persistence.Context;
using Auth.Infrastructure.Security.Encryption;
using Auth.Infrastructure.Security.Passwords;
using Auth.IntegrationEvents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedRabbitMQ;
using SharedRabbitMQ.Publishing;

namespace Auth.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistence(configuration);
            services.AddDomainServices();
            services.AddDataEncryption();
            services.AddServiceConfiguration();
            services.AddSpecifications();
            services.AddEventBus();
            services.AddMediator();
            services.AddTokenGenerator();
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
        }

        private static void AddSpecifications(this IServiceCollection services)
        {
            services.AddTransient<IEmailExists, EmailExists>();
            services.AddTransient<IEmailExistsOnOtherUsers, EmailExistsOnOtherUsers>();
            services.AddTransient<IUsernameExists, UsernameExists>();
            services.AddTransient<IUsernameExistsOnOtherUsers, UsernameExistsOnOtherUsers>();
            services.AddTransient<IPasswordMatches, PasswordMatches>();
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

        private static void AddEventBus(this IServiceCollection services)
        {
            services.AddRabbit();
            services.AddTransient<IEventPublisher<UserRegisteredEvent>, UserRegisteredEventPublisher>();
            services.AddTransient<IEventPublisher<UserResetPasswordEvent>, UserResetPasswordEventPublisher>();
            services.AddTransient<IEventPublisher<UsernameReminderRequestedEvent>, UsernameReminderRequestedEventPublisher>();
            services.AddTransient<IEventPublisher<UserDeletedEvent>, UserDeletedEventPublisher>();
        }
    }
}
