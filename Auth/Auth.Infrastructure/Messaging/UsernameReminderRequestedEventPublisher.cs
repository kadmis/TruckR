using Auth.IntegrationEvents;
using SharedRabbitMQ.Externals.Events;
using SharedRabbitMQ.Publishing;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Messaging
{
    public class UsernameReminderRequestedEventPublisher : IEventPublisher<UsernameReminderRequestedEvent>
    {
        private readonly IEventBusClient _client;

        public UsernameReminderRequestedEventPublisher(IEventBusClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public Task Publish(UsernameReminderRequestedEvent @event, CancellationToken cancellationToken = default)
        {
            return _client.Publish(@event, cancellationToken);
        }
    }
}
