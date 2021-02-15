using Auth.API.Infrastructure.Commands;
using Auth.API.Infrastructure.Models.CommandsResults;
using Auth.Domain.Services.UserOperations;
using Auth.IntegrationEvents;
using MediatR;
using SharedRabbitMQ.Publishing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.API.Infrastructure.Handlers
{
    public class RemindUsernameCommandHandler : IRequestHandler<RemindUsernameCommand, RemindUsernameResult>
    {
        private readonly IUserOperationsService _service;
        private readonly IEventPublisher<UsernameRemindedEvent> _publisher;

        public RemindUsernameCommandHandler(IUserOperationsService service, IEventPublisher<UsernameRemindedEvent> publisher)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
        }

        public async Task<RemindUsernameResult> Handle(RemindUsernameCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _service.RemindUsername(request.Email, cancellationToken);
                await _publisher.Publish(new UsernameRemindedEvent(result.Email.Value, result.Username.Value), cancellationToken);

                return RemindUsernameResult.Success();
            }
            catch(Exception ex)
            {
                return RemindUsernameResult.Fail(ex.Message);
            }
        }
    }
}
