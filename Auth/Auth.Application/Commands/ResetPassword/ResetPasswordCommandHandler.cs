using Auth.Domain.Exceptions.UserExceptions;
using Auth.Domain.Persistence;
using Auth.IntegrationEvents;
using BuildingBlocks.Application.Handlers;
using BuildingBlocks.Application.Identity;
using BuildingBlocks.EventBus.Externals.Events.Publishing;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Application.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : ICommandHandler<ResetPasswordCommand, ResetPasswordResult>
    {
        private readonly IIdentityAccessor _identity;
        private readonly IEventPublisher<UserResetPasswordEvent> _publisher;
        private readonly IUnitOfWork _uow;

        public ResetPasswordCommandHandler(
            IEventPublisher<UserResetPasswordEvent> publisher,
            IUnitOfWork uow,
            IIdentityAccessor identity)
        {
            _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentException(nameof(identity));
        }

        public async Task<ResetPasswordResult> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var id = _identity.UserIdentity().UserId;

                var user = await _uow.UserRepository.FindById(id, cancellationToken);

                if (user == null)
                {
                    throw new UserDoesntExistException();
                }

                user.ResetPassword();

                await _uow.Save(cancellationToken);

                await _publisher.Publish(new UserResetPasswordEvent(user.Email.Value, user.PasswordResetToken.Value, user.Id), cancellationToken);

                return ResetPasswordResult.Success();
            }
            catch (Exception ex)
            {
                return ResetPasswordResult.Fail(ex.Message);
            }
        }
    }
}
