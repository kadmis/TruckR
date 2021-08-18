using Auth.IntegrationEvents;
using BuildingBlocks.Application.Commands;
using BuildingBlocks.Application.Handlers;
using Email.API.Infrastructure.Database;
using Email.API.Infrastructure.Database.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Email.API.Infrastructure.InternalCommands.SendPasswordResetEmail
{
    public class SendPasswordResetEmailCommand : ICommand
    {
        public string Email { get; }
        public Guid ResetToken { get; }

        public SendPasswordResetEmailCommand(UserResetPasswordEvent @event)
        {
            Email = @event.Email;
            ResetToken = @event.ResetToken;
        }
    }

    public class SendPasswordResetEmailCommandHandler : ICommandHandler<SendPasswordResetEmailCommand>
    {
        private readonly IEmailQueueService _emailQueueService;

        public SendPasswordResetEmailCommandHandler(IEmailQueueService emailQueueService)
        {
            _emailQueueService = emailQueueService;
        }

        public async Task<Unit> Handle(SendPasswordResetEmailCommand request, CancellationToken cancellationToken)
        {
            await _emailQueueService.Add(new EmailQueueItem(
                request.Email,
                request.Email,
                "Password has been reset.",
                $"Your password has been reset. Use this token to set it again: {request.ResetToken}"), cancellationToken);

            return Unit.Value;
        }
    }
}
