using Auth.IntegrationEvents;
using BuildingBlocks.Application.Commands;
using BuildingBlocks.Application.Handlers;
using Email.API.Infrastructure.Database;
using Email.API.Infrastructure.Database.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Email.API.Infrastructure.InternalCommands.SendUserDeletionEmail
{
    public class SendUserDeletionEmailCommand : ICommand
    {
        public string Email { get; }

        public SendUserDeletionEmailCommand(UserDeletedEvent @event)
        {
            Email = @event.Email;
        }
    }

    public class SendUserDeletionEmailCommandHandler : ICommandHandler<SendUserDeletionEmailCommand>
    {
        private readonly IEmailQueueService _emailQueueService;

        public SendUserDeletionEmailCommandHandler(IEmailQueueService emailQueueService)
        {
            _emailQueueService = emailQueueService;
        }

        public async Task<Unit> Handle(SendUserDeletionEmailCommand request, CancellationToken cancellationToken)
        {
            await _emailQueueService.Add(new EmailQueueItem(
                request.Email,
                request.Email,
                "You account has been deleted.",
                "Your account has been deleted. Contact administration for details."), cancellationToken);

            return Unit.Value;
        }
    }
}
