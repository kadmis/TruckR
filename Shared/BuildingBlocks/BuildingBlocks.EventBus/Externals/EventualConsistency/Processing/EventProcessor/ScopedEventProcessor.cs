using BuildingBlocks.Application.Commands;
using BuildingBlocks.EventBus.Externals.Events;
using BuildingBlocks.EventBus.Externals.EventualConsistency.Processing.EventMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BuildingBlocks.EventBus.Externals.EventualConsistency.Processing.EventProcessor
{
    public class ScopedEventProcessor : IEventProcessor
    {
        private readonly IServiceProvider _serviceProvider;

        public ScopedEventProcessor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<bool> SaveEvent<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
            where TEvent : IEvent
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();

                var eventService = scope.ServiceProvider.GetRequiredService<IEventStore>();

                await eventService.Add(@event, cancellationToken);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> ProcessEvent<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
            where TEvent : IEvent
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();

                var mapper = scope.ServiceProvider.GetRequiredService<EventToCommandMapper>();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                var command = mapper.RetrieveCommand(@event);

                if (command == null)
                    return false;

                await mediator.Send(command, cancellationToken);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
