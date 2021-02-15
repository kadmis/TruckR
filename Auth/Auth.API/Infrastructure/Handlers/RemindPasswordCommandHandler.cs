using Auth.API.Infrastructure.Commands;
using Auth.API.Infrastructure.Models.CommandsResults;
using Auth.Domain.Services.UserOperations;
using Auth.IntegrationEvents;
using MediatR;
using SharedRabbitMQ.Publishing;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.API.Infrastructure.Handlers
{
    public class RemindPasswordCommandHandler : IRequestHandler<RemindPasswordCommand, RemindPasswordResult>
    {
        private readonly IUserOperationsService _service;
        private readonly IEventPublisher<UserResetPasswordEvent> _publisher;

        public RemindPasswordCommandHandler(IUserOperationsService service, IEventPublisher<UserResetPasswordEvent> publisher)
        {
            _service = service;
            _publisher = publisher;
        }

        public async Task<RemindPasswordResult> Handle(RemindPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _service.RemindPassword(request.Email, request.Username, cancellationToken);
                await _publisher.Publish(new UserResetPasswordEvent(user.Email.Value, user.PasswordResetToken.Value, user.Id));

                return RemindPasswordResult.Success();
            }
            catch(Exception ex)
            {
                return RemindPasswordResult.Fail(ex.Message);
            }
        }
    }
}
