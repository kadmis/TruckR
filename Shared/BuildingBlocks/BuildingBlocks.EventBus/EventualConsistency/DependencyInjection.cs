using BuildingBlocks.EventBus.EventualConsistency.Database.EFCore;
using BuildingBlocks.EventBus.EventualConsistency.Processing;
using BuildingBlocks.EventBus.EventualConsistency.Processing.EFCore;
using BuildingBlocks.EventBus.EventualConsistency.Processing.EventProcessor;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.EventBus.EventualConsistency
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
