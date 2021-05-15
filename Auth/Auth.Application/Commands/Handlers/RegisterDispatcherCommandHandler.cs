using Auth.Application.Models.Results;
using Auth.Domain.Services.Registration;
using Auth.IntegrationEvents;
using BuildingBlocks.Application.Handlers;
using BuildingBlocks.EventBus.Externals.Events.Publishing;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Application.Commands.Handlers
{
    public class RegisterDispatcherCommandHandler : ICommandHandler<RegisterDispatcherCommand, RegistrationResult>
    {
        private readonly IUserRegistrationService _registrationService;
        private readonly IEventPublisher<UserRegisteredEvent> _eventPublisher;

        public RegisterDispatcherCommandHandler(IUserRegistrationService registrationService, IEventPublisher<UserRegisteredEvent> eventPublisher)
        {
            _registrationService = registrationService ?? throw new ArgumentNullException(nameof(registrationService));
            _eventPublisher = eventPublisher ?? throw new ArgumentNullException(nameof(eventPublisher)); 
        }

        public async Task<RegistrationResult> Handle(RegisterDispatcherCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _registrationService.RegisterDispatcher(
                    request.Username,
                    request.Firstname,
                    request.Lastname,
                    request.Password,
                    request.Email,
                    request.PhoneNumber,
                    cancellationToken);

                await _eventPublisher.Publish(new UserRegisteredEvent(
                    result.Id,
                    result.ActivationId.Value,
                    result.Email.Value,
                    result.Firstname,
                    result.Lastname,
                    result.Role.Value), cancellationToken);

                return RegistrationResult.Success(result.Id);
            }
            catch (Exception ex)
            {
                return RegistrationResult.Fail(ex.Message);
            }
        }
    }
}
