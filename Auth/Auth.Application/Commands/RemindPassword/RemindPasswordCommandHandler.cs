using Auth.Application.Models.Results;
using Auth.Domain.Data.ValueObjects;
using Auth.Domain.Exceptions.UserExceptions;
using Auth.Domain.Persistence;
using Auth.IntegrationEvents;
using BuildingBlocks.Application.Handlers;
using BuildingBlocks.EventBus.Externals.Events.Publishing;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Application.Commands.RemindPassword
{
    public class RemindPasswordCommandHandler : ICommandHandler<RemindPasswordCommand, RemindPasswordResult>
    {
        private readonly IEventPublisher<UserResetPasswordEvent> _publisher;
        private readonly IUnitOfWork _uow;

        public RemindPasswordCommandHandler(IEventPublisher<UserResetPasswordEvent> publisher, IUnitOfWork uow)
        {
            _publisher = publisher;
            _uow = uow;
        }

        public async Task<RemindPasswordResult> Handle(RemindPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var givenEmail = new Email(request.Email);
                var givenUsername = new Username(request.Username);

                var user = await _uow.UserRepository.FindByEmailAndUsername(givenEmail, givenUsername, cancellationToken);
                if (user == null)
                {
                    throw new UserDoesntExistException();
                }

                user.ResetPassword();
                await _uow.Save(cancellationToken);

                await _publisher.Publish(new UserResetPasswordEvent(user.Email.Value, user.PasswordResetToken.Value, user.Id), cancellationToken);

                return RemindPasswordResult.Success();
            }
            catch (Exception ex)
            {
                return RemindPasswordResult.Fail(ex.Message);
            }
        }
    }
}
