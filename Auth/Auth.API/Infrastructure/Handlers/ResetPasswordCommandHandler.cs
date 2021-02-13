using Auth.API.Infrastructure.Commands;
using Auth.API.Infrastructure.Models.CommandsResults;
using Auth.Domain.Services.UserOperations;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.API.Infrastructure.Handlers
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ResetPasswordResult>
    {
        private readonly IUserOperationsService _userOperationsService;

        public ResetPasswordCommandHandler(IUserOperationsService userOperationsService)
        {
            _userOperationsService = userOperationsService ?? throw new ArgumentNullException(nameof(userOperationsService));
        }

        public async Task<ResetPasswordResult> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _userOperationsService.ResetPassword(request.Id, cancellationToken);
                return ResetPasswordResult.Success();
            }
            catch(Exception ex)
            {
                return ResetPasswordResult.Fail(ex.Message);
            }
        }
    }
}
