using Auth.API.Infrastructure.Commands;
using Auth.API.Infrastructure.Models.CommandsResults;
using Auth.Domain.Services.UserOperations;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.API.Infrastructure.Handlers
{
    public class ActivateUserCommandHandler : IRequestHandler<ActivateUserCommand, UserActivationResult>
    {
        private readonly IUserOperationsService _service;

        public ActivateUserCommandHandler(IUserOperationsService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<UserActivationResult> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _service.Activate(request.UserId, request.ActivationId, cancellationToken);
                return UserActivationResult.Success();
            }
            catch(Exception ex)
            {
                return UserActivationResult.Fail(ex.Message);
            }
        }
    }
}
