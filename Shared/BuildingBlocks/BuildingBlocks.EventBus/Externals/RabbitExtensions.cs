using Microsoft.Extensions.DependencyInjection;
using RawRabbit.Instantiation;
using RawRabbit.DependencyInjection.ServiceCollection;
using Microsoft.AspNetCore.Builder;
using System;
using BuildingBlocks.EventBus.Externals.Events.Handling;
using BuildingBlocks.EventBus.Externals.Events;
using BuildingBlocks.EventBus.Internals;

namespace BuildingBlocks.EventBus.Externals
{
    public static class RabbitExtensions
    {
        public static void AddRabbit(this IServiceCollection services)
        {
            services.AddRawRabbit(new RawRabbitOptions
            {
                ClientConfiguration = Configuration.Create()
            });
            services.AddSingleton<IEventBusClient, RabbitClient>();
        }

        public static IApplicationBuilder AddHandler<T>(this IApplicationBuilder app, IEventBusClient client) where T : IEvent
        {
            if (app.ApplicationServices.GetService(typeof(IEventHandler<T>)) is not IEventHandler<T> handler)
                throw new NullReferenceException(nameof(IEventHandler<T>));

            client
                .Subscribe<T>(async (@event) =>
                {
                    await handler.Handle(@event);
                });

            return app;
        }

        public static IApplicationBuilder AddHandler<T>(this IApplicationBuilder app) where T : IEvent
        {
            if (app.ApplicationServices.GetService(typeof(IEventBusClient)) is not IEventBusClient busClient)
                throw new NullReferenceException();

            return app.AddHandler<T>(busClient);
        }
    }
}
