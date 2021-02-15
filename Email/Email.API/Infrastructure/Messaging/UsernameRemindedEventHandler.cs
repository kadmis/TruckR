using Auth.IntegrationEvents;
using Email.API.Infrastructure.Models;
using Email.API.Infrastructure.Services;
using SharedRabbitMQ.Externals.Events.Handling;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Email.API.Infrastructure.Messaging
{
    public class UsernameRemindedEventHandler : IEventHandler<UsernameRemindedEvent>
    {
        private readonly IEmailService _service;

        public UsernameRemindedEventHandler(IEmailService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task Handle(UsernameRemindedEvent @event, CancellationToken cancellationToken = default)
        {
            await _service.SendEmail(new EmailModel()
            {
                Message = $"Your username is: {@event.Username}",
                RecipientAddress = @event.Email,
                RecipientName = @event.Email,
                Title = "Reminded username."
            }, cancellationToken);
        }
    }
}
