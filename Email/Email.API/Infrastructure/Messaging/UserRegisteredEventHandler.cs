using Auth.IntegrationEvents;
using Email.API.Infrastructure.Models;
using Email.API.Infrastructure.Services;
using SharedRabbitMQ.Externals.Events.Handling;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Email.API.Infrastructure.Messaging
{
    public class UserRegisteredEventHandler : IEventHandler<UserRegisteredEvent>
    {
        private readonly IEmailService _emailService;

        public UserRegisteredEventHandler(IEmailService emailService)
        {
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }

        public async Task Handle(UserRegisteredEvent @event, CancellationToken cancellationToken = default)
        {
            await _emailService.SendEmail(new EmailModel()
            {
                Message = $"Activate your account here: https://localhost:44312/api/user/activate/{@event.Id}/{@event.ActivationId}",
                RecipientAddress = @event.Email.Value,
                RecipientName = @event.Email.Value,
                Title = "Welcome to the TruckR system."
            }, cancellationToken);
        }
    }
}
