using Auth.Domain.Data.ValueObjects;
using Auth.Domain.Exceptions.UserExceptions;
using Auth.Domain.Persistence;
using Auth.IntegrationEvents;
using BuildingBlocks.Application.Handlers;
using BuildingBlocks.EventBus.Externals.Events.Publishing;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Application.Commands.Delete
{
    public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand, DeleteUserResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventPublisher<UserDeletedEvent> _publisher;

        public DeleteUserCommandHandler(IUnitOfWork unitOfWork, IEventPublisher<UserDeletedEvent> publisher)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
        }

        public async Task<DeleteUserResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = request.Id;

                var user = await _unitOfWork.UserRepository.FindById(userId, cancellationToken);

                if (user == null)
                {
                    throw new UserDoesntExistException();
                }

                var emailCopy = new Email(user.Email);
                user.Delete();

                await _unitOfWork.Save(cancellationToken);
                await _publisher.Publish(new UserDeletedEvent(userId, emailCopy.Value), cancellationToken);

                return DeleteUserResult.Success();
            }
            catch (Exception ex)
            {
                return DeleteUserResult.Fail(ex.Message);
            }
        }
    }
}
