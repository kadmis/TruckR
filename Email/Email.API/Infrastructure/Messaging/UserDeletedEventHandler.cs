using Auth.IntegrationEvents;
using Email.API.Infrastructure.Models;
using Email.API.Infrastructure.Services;
using SharedRabbitMQ.Externals.Events.Handling;
using System.Threading;
using System.Threading.Tasks;

namespace Email.API.Infrastructure.Messaging
{
    public class UserDeletedEventHandler : IEventHandler<UserDeletedEvent>
    {
        private readonly IEmailService _service;

        public UserDeletedEventHandler(IEmailService service)
        {
            _service = service;
        }

        public Task Handle(UserDeletedEvent @event, CancellationToken cancellationToken = default)
        {
            return _service.SendEmail(new EmailModel()
            {
                RecipientAddress = @event.Email.Value,
                Message = "Your account has been deleted. Contact administration for details.",
                RecipientName = @event.Email.Value,
                Title = "You account has been deleted."
            }, cancellationToken);
        }
    }
}
