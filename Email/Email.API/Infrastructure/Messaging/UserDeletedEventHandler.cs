using Auth.IntegrationEvents;
using BuildingBlocks.EventBus.Externals.Events.Handling;
using Email.API.Infrastructure.Models;
using Email.API.Infrastructure.Services;
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
                RecipientAddress = @event.Email,
                Message = "Your account has been deleted. Contact administration for details.",
                RecipientName = @event.Email,
                Title = "You account has been deleted."
            }, cancellationToken);
        }
    }
}
