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
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ResetPasswordResult>
    {
        private readonly IUserOperationsService _userOperationsService;
        private readonly IEventPublisher<UserResetPasswordEvent> _publisher;

        public ResetPasswordCommandHandler(IUserOperationsService userOperationsService, IEventPublisher<UserResetPasswordEvent> publisher)
        {
            _userOperationsService = userOperationsService ?? throw new ArgumentNullException(nameof(userOperationsService));
            _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
        }

        public async Task<ResetPasswordResult> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userOperationsService.ResetPassword(request.Id, cancellationToken);
                await _publisher.Publish(new UserResetPasswordEvent(user.Email.Value, user.PasswordResetToken.Value, user.Id), cancellationToken);

                return ResetPasswordResult.Success();
            }
            catch(Exception ex)
            {
                return ResetPasswordResult.Fail(ex.Message);
            }
        }
    }
}
