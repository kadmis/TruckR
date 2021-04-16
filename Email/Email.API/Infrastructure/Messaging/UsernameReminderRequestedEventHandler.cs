using Auth.IntegrationEvents;
using Email.API.Infrastructure.Models;
using Email.API.Infrastructure.Services;
using SharedRabbitMQ.Externals.Events.Handling;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Email.API.Infrastructure.Messaging
{
    public class UsernameReminderRequestedEventHandler : IEventHandler<UsernameReminderRequestedEvent>
    {
        private readonly IEmailService _service;

        public UsernameReminderRequestedEventHandler(IEmailService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public Task Handle(UsernameReminderRequestedEvent @event, CancellationToken cancellationToken = default)
        {
            return _service.SendEmail(new EmailModel()
            {
                Message = $"Your username is: {@event.Username.Value}",
                RecipientAddress = @event.Email.Value,
                RecipientName = @event.Email.Value,
                Title = "Reminded username."
            }, cancellationToken);
        }
    }
}
