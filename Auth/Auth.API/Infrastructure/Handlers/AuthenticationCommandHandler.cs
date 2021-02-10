using Auth.API.Infrastructure.Commands;
using Auth.API.Infrastructure.JWT;
using Auth.API.Infrastructure.Models;
using Auth.Domain.Data.ValueObjects;
using Auth.Domain.Persistence;
using Auth.Domain.Persistence.Repositories;
using Auth.Domain.Security.Passwords;
using Auth.Domain.Services.Authentication;
using Auth.Domain.Specifications.Password;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.API.Infrastructure.Handlers
{
    public class AuthenticationCommandHandler : IRequestHandler<AuthenticationCommand, AuthenticationResult>
    {
        private readonly IUserAuthenticationService _authService;
        private readonly ITokenGenerator _tokenGenerator;

        public AuthenticationCommandHandler(IUserAuthenticationService authService, ITokenGenerator tokenGenerator)
        {
            _authService = authService;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<AuthenticationResult> Handle(AuthenticationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _authService.Authenticate(request.Username, request.Password, cancellationToken);
                return AuthenticationResult.Success(_tokenGenerator.GenerateFor(user));
            }
            catch(Exception ex)
            {
                return AuthenticationResult.Fail(ex.Message);
            }
        }
    }
}
