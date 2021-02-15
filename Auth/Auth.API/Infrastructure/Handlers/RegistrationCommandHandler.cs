using Auth.API.Infrastructure.Commands;
using Auth.API.Infrastructure.Models.CommandsResults;
using Auth.Domain.Services.Registration;
using Auth.IntegrationEvents;
using MediatR;
using SharedRabbitMQ.Publishing;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.API.Infrastructure.Handlers
{
    public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, RegistrationResult>
    {
        private readonly IUserRegistrationService _registrationService;
        private readonly IEventPublisher<UserRegisteredEvent> _eventPublisher;

        public RegistrationCommandHandler(IUserRegistrationService registrationService, IEventPublisher<UserRegisteredEvent> eventPublisher)
        {
            _registrationService = registrationService ?? throw new ArgumentNullException(nameof(registrationService));
            _eventPublisher = eventPublisher ?? throw new ArgumentNullException(nameof(eventPublisher));
        }

        public async Task<RegistrationResult> Handle(RegistrationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _registrationService.Register(request.Username, request.Password, request.Email, cancellationToken);

                await _eventPublisher.Publish(new UserRegisteredEvent(result.Id, result.ActivationId.Value, result.Email.Value), cancellationToken);

                return RegistrationResult.Success(result.Id);
            }
            catch(Exception ex)
            {
                return RegistrationResult.Fail(ex.Message);
            }
        }
    }
}
