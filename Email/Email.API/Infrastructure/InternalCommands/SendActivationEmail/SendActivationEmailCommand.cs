using Auth.IntegrationEvents;
using BuildingBlocks.Application.Commands;
using BuildingBlocks.Application.Handlers;
using Email.API.Infrastructure.Database;
using Email.API.Infrastructure.Database.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Email.API.Infrastructure.InternalCommands.SendActivationEmail
{
    public class SendActivationEmailCommand : ICommand
    {
        public Guid UserId { get; }
        public Guid ActivationId { get; }
        public string Email { get; }

        public SendActivationEmailCommand(UserRegisteredEvent @event)
        {
            UserId = @event.UserId;
            ActivationId = @event.ActivationId;
            Email = @event.Email;
        }
    }

    public class SendActivationEmailCommandHandler : ICommandHandler<SendActivationEmailCommand>
    {
        private readonly IEmailQueueService _emailQueueService;

        public SendActivationEmailCommandHandler(IEmailQueueService emailQueueService)
        {
            _emailQueueService = emailQueueService;
        }

        public async Task<Unit> Handle(SendActivationEmailCommand request, CancellationToken cancellationToken)
        {
            await _emailQueueService.Add(new EmailQueueItem(
                request.Email,
                request.Email,
                "Welcome to the TruckR system.",
                $"Activate your account here: http://localhost:4200/#/activate/{request.UserId}/{request.ActivationId}"), cancellationToken);

            return Unit.Value;
        }
    }
}
