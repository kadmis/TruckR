using BuildingBlocks.EventBus.Externals.EventualConsistency.Database.EFCore;
using BuildingBlocks.EventBus.Externals.EventualConsistency.Processing;
using BuildingBlocks.EventBus.Externals.EventualConsistency.Processing.EFCore;
using BuildingBlocks.EventBus.Externals.EventualConsistency.Processing.EventProcessor;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.EventBus.Externals.EventualConsistency
{
    public static class DependencyInjection
    {
        public static void AddEventContext<TContext>(this IServiceCollection services) where TContext : IEventContext
        {
            services.AddScoped<IEventContext>(provider => provider.GetService<TContext>());
        }

        public static void AddEventServices(this IServiceCollection services)
        {
            services.AddTransient<IEventStore, EventStore>();
            services.AddTransient<IMainEventProcessor, MainEventProcessor>();
            services.AddTransient<IEventProcessor, ScopedEventProcessor>();
        }
    }
}
