using Auth.IntegrationEvents;
using Email.API.Infrastructure.Models;
using Email.API.Infrastructure.Services;
using SharedRabbitMQ.Externals.Events.Handling;
using System.Threading;
using System.Threading.Tasks;

namespace Email.API.Infrastructure.Messaging
{
    public class UserResetPasswordEventHandler : IEventHandler<UserResetPasswordEvent>
    {
        private readonly IEmailService _service;

        public UserResetPasswordEventHandler(IEmailService service)
        {
            _service = service;
        }

        public async Task Handle(UserResetPasswordEvent @event, CancellationToken cancellationToken = default)
        {
            await _service.SendEmail(new EmailModel
            {
                Message = $"Your password has been reset. Use this token to set it again: {@event.ResetToken}",
                RecipientAddress = @event.Email,
                RecipientName = @event.Email,
                Title = "Password has been reset."
            }, cancellationToken);
        }
    }
}
