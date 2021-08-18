using Auth.IntegrationEvents;
using BuildingBlocks.Application.Commands;
using BuildingBlocks.Application.Handlers;
using Email.API.Infrastructure.Database;
using Email.API.Infrastructure.Database.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Email.API.Infrastructure.InternalCommands.SendUsernameReminderEmail
{
    public class SendUsernameReminderEmailCommand : ICommand
    {
        public string Email { get; }
        public string Username { get; }
        public SendUsernameReminderEmailCommand(UsernameReminderRequestedEvent @event)
        {
            Email = @event.Email;
            Username = @event.Username;
        }
    }

    public class SendUsernameReminderEmailCommandHandler : ICommandHandler<SendUsernameReminderEmailCommand>
    {
        private readonly IEmailQueueService _emailQueueService;

        public SendUsernameReminderEmailCommandHandler(IEmailQueueService emailQueueService)
        {
            _emailQueueService = emailQueueService;
        }

        public async Task<Unit> Handle(SendUsernameReminderEmailCommand request, CancellationToken cancellationToken)
        {
            await _emailQueueService.Add(new EmailQueueItem(
                request.Email,
                request.Email,
                "Reminded username.",
                $"Your username is: {request.Username}"), cancellationToken);

            return Unit.Value;
        }
    }
}
