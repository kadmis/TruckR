using Auth.Application.Commands.RegisterDispatcher;
using Auth.Domain.Services.Registration;
using Auth.IntegrationEvents;
using BuildingBlocks.Application.Handlers;
using BuildingBlocks.EventBus.Externals.Events.Publishing;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Application.Commands.RegisterDriver
{
    public class RegisterDriverCommandHandler : ICommandHandler<RegisterDriverCommand, RegisterDriverResult>
    {
        private readonly IUserRegistrationService _registrationService;
        private readonly IEventPublisher<UserRegisteredEvent> _eventPublisher;

        public RegisterDriverCommandHandler(IUserRegistrationService registrationService, IEventPublisher<UserRegisteredEvent> eventPublisher)
        {
            _registrationService = registrationService ?? throw new ArgumentNullException(nameof(registrationService));
            _eventPublisher = eventPublisher ?? throw new ArgumentNullException(nameof(eventPublisher));
        }

        public async Task<RegisterDriverResult> Handle(RegisterDriverCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _registrationService.RegisterDriver(
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

                return RegisterDriverResult.Success(result.Id);
            }
            catch (Exception ex)
            {
                return RegisterDriverResult.Fail(ex.Message);
            }
        }
    }
}
