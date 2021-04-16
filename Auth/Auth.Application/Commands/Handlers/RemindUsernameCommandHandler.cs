using Auth.Application.Models.Results;
using Auth.Domain.Data.ValueObjects;
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
    public class RemindUsernameCommandHandler : ICommandHandler<RemindUsernameCommand, RemindUsernameResult>
    {
        private readonly IEventPublisher<UsernameReminderRequestedEvent> _publisher;
        private readonly IUnitOfWork _uow;

        public RemindUsernameCommandHandler(IEventPublisher<UsernameReminderRequestedEvent> publisher, IUnitOfWork uow)
        {
            _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<RemindUsernameResult> Handle(RemindUsernameCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var givenEmail = new Email(request.Email);

                var user = await _uow.UserRepository.FindByEmail(givenEmail, cancellationToken);
                if (user == null)
                {
                    throw new UserDoesntExistException();
                }

                await _publisher.Publish(new UsernameReminderRequestedEvent(user.Email, user.Username), cancellationToken);

                return RemindUsernameResult.Success();
            }
            catch (Exception ex)
            {
                return RemindUsernameResult.Fail(ex.Message);
            }
        }
    }
}
