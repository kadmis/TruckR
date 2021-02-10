using Auth.API.Infrastructure.Commands;
using Auth.API.Infrastructure.Models.CommandsResults;
using Auth.Domain.Services.UserOperations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.API.Infrastructure.Handlers
{
    public class ActivateUserCommandHandler : IRequestHandler<ActivateUserCommand, UserActivationResult>
    {
        private readonly IUserOperationsService _service;

        public ActivateUserCommandHandler(IUserOperationsService service)
        {
            _service = service;
        }

        public async Task<UserActivationResult> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _service.Activate(request.UserId, request.ActivationId);
                return UserActivationResult.Success();
            }
            catch(Exception ex)
            {
                return UserActivationResult.Fail(ex.Message);
            }
        }
    }
}
