using Auth.IntegrationEvents;
using BuildingBlocks.EventBus.Externals.Events;
using BuildingBlocks.EventBus.Externals.Events.Publishing;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Messaging
{
    public class UserResetPasswordEventPublisher : IEventPublisher<UserResetPasswordEvent>
    {
        private readonly IEventBusClient _client;

        public UserResetPasswordEventPublisher(IEventBusClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public Task Publish(UserResetPasswordEvent @event, CancellationToken cancellationToken = default)
        {
            return _client.Publish(@event, cancellationToken);
        }
    }
}
