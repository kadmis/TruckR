using Auth.IntegrationEvents;
using SharedRabbitMQ.Externals.Events.Handling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Transport.Infrastructure.Messaging
{
    public class UserRegisteredEventHandler : IEventHandler<UserRegisteredEvent>
    {
        public Task Handle(UserRegisteredEvent @event, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
