using Auth.Application.Models.Results;
using Auth.Domain.Data.Entities;
using Auth.Domain.Data.ValueObjects;
using Auth.Domain.Exceptions.UserExceptions;
using Auth.Domain.Persistence;
using Auth.IntegrationEvents;
using BuildingBlocks.Application.Handlers;
using BuildingBlocks.EventBus.Externals.Events.Publishing;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Application.Commands.Activate
{
    public class ActivateUserCommandHandler : ICommandHandler<ActivateUserCommand, UserActivationResult>
    {
        private readonly IUnitOfWork _uow;
        private readonly IEventPublisher<DriverActivatedEvent> _driverPublisher;
        private readonly IEventPublisher<DispatcherActivatedEvent> _dispatcherPublisher;

        public ActivateUserCommandHandler(
            IUnitOfWork uow,
            IEventPublisher<DriverActivatedEvent> driverPublisher,
            IEventPublisher<DispatcherActivatedEvent> dispatcherPublisher)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _driverPublisher = driverPublisher ?? throw new ArgumentNullException(nameof(driverPublisher));
            _dispatcherPublisher = dispatcherPublisher ?? throw new ArgumentNullException(nameof(dispatcherPublisher));
        }

        public async Task<UserActivationResult> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _uow.UserRepository.FindById(request.UserId, cancellationToken);

                if (user == null)
                {
                    throw new UserDoesntExistException();
                }

                user.Activate(request.ActivationId);

                await _uow.Save(cancellationToken);

                await PublishActivatedEvent(user);

                return UserActivationResult.Success();
            }
            catch (Exception ex)
            {
                return UserActivationResult.Fail(ex.Message);
            }
        }

        private async Task PublishActivatedEvent(User user)
        {
            if (user.Role.Equals(UserRole.Driver))
                await _driverPublisher.Publish(new DriverActivatedEvent(user.Id));
            else if (user.Role.Equals(UserRole.Dispatcher))
                await _dispatcherPublisher.Publish(new DispatcherActivatedEvent(user.Id));
        }
    }
}
