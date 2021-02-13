using Auth.API.Infrastructure.Commands;
using Auth.API.Infrastructure.Models.CommandsResults;
using Auth.Domain.Services.Registration;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.API.Infrastructure.Handlers
{
    public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, RegistrationResult>
    {
        private readonly IUserRegistrationService _registrationService;

        public RegistrationCommandHandler(IUserRegistrationService registrationService)
        {
            _registrationService = registrationService ?? throw new ArgumentNullException(nameof(registrationService));
        }

        public async Task<RegistrationResult> Handle(RegistrationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _registrationService.Register(request.Username, request.Password, request.Email, cancellationToken);
                return RegistrationResult.Success(result.Id);
            }
            catch(Exception ex)
            {
                return RegistrationResult.Fail(ex.Message);
            }
        }
    }
}
