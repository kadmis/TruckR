using Auth.Application.Commands;
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

namespace Auth.Application.Commands.Handlers
{
    public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand, UserDeletionResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventPublisher<UserDeletedEvent> _publisher;

        public DeleteUserCommandHandler(IUnitOfWork unitOfWork, IEventPublisher<UserDeletedEvent> publisher)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
        }

        public async Task<UserDeletionResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
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

                return UserDeletionResult.Success(userId);
            }
            catch (Exception ex)
            {
                return UserDeletionResult.Fail(ex.Message);
            }
        }
    }
}
