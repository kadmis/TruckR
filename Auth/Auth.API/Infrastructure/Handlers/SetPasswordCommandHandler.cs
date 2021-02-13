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
    public class SetPasswordCommandHandler : IRequestHandler<SetPasswordCommand, SetPasswordResult>
    {
        private readonly IUserOperationsService _service;

        public SetPasswordCommandHandler(IUserOperationsService service)
        {
            _service = service;
        }

        public async Task<SetPasswordResult> Handle(SetPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _service.SetPassword(request.UserId, request.ResetToken, request.Password, cancellationToken);

                return SetPasswordResult.Success();
            }
            catch(Exception ex)
            {
                return SetPasswordResult.Fail(ex.Message);
            }
        }
    }
}
