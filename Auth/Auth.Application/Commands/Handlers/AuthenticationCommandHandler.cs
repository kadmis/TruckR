using Auth.Application.Commands;
using Auth.Application.JWT;
using Auth.Application.Models.Results;
using Auth.Domain.Services.Authentication;
using BuildingBlocks.Application.Handlers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Application.Commands.Handlers
{
    public class AuthenticationCommandHandler : ICommandHandler<AuthenticationCommand, AuthenticationResult>
    {
        private readonly IUserAuthenticationService _authService;
        private readonly ITokenGenerator _tokenGenerator;

        public AuthenticationCommandHandler(IUserAuthenticationService authService, ITokenGenerator tokenGenerator)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _tokenGenerator = tokenGenerator ?? throw new ArgumentNullException(nameof(tokenGenerator));
        }

        public async Task<AuthenticationResult> Handle(AuthenticationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _authService.Authenticate(request.Username, request.Password, cancellationToken);
                return AuthenticationResult.Success(_tokenGenerator.GenerateFor(user));
            }
            catch (Exception ex)
            {
                return AuthenticationResult.Fail(ex.Message);
            }
        }
    }
}
