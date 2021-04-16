using Auth.Application.Models.Results;
using Auth.Domain.Exceptions.UserExceptions;
using Auth.Domain.Persistence;
using Auth.IntegrationEvents;
using BuildingBlocks.Application.Handlers;
using SharedRabbitMQ.Publishing;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Application.Commands.Handlers
{
    public class ResetPasswordCommandHandler : ICommandHandler<ResetPasswordCommand, ResetPasswordResult>
    {
        private readonly IEventPublisher<UserResetPasswordEvent> _publisher;
        private readonly IUnitOfWork _uow;

        public ResetPasswordCommandHandler(IEventPublisher<UserResetPasswordEvent> publisher, IUnitOfWork uow)
        {
            _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<ResetPasswordResult> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _uow.UserRepository.FindById(request.Id, cancellationToken);

                if (user == null)
                {
                    throw new UserDoesntExistException();
                }

                user.ResetPassword();

                await _uow.Save(cancellationToken);

                await _publisher.Publish(new UserResetPasswordEvent(user.Email, user.PasswordResetToken.Value, user.Id), cancellationToken);

                return ResetPasswordResult.Success();
            }
            catch (Exception ex)
            {
                return ResetPasswordResult.Fail(ex.Message);
            }
        }
    }
}
