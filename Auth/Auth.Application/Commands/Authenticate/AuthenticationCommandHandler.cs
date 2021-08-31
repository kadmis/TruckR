using Auth.Domain.Data.Entities;
using Auth.Domain.Data.ValueObjects;
using Auth.Domain.Persistence;
using Auth.Domain.Services.TokenGeneration;
using Auth.Domain.Specifications.PasswordSpecifications.Interfaces;
using BuildingBlocks.Application.Handlers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Application.Commands.Authenticate
{
    public class AuthenticationCommandHandler : ICommandHandler<AuthenticationCommand, AuthenticationResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordMatches _passwordMatches;
        private readonly ITokenGenerator _tokenGenerator;

        public AuthenticationCommandHandler(
            ITokenGenerator tokenGenerator,
            IUnitOfWork unitOfWork,
            IPasswordMatches passwordMatches)
        {
            _tokenGenerator = tokenGenerator ?? throw new ArgumentNullException(nameof(tokenGenerator));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _passwordMatches = passwordMatches ?? throw new ArgumentNullException(nameof(passwordMatches));
        }

        public async Task<AuthenticationResult> Handle(AuthenticationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var givenUsername = new Username(request.Username);
                var givenPassword = new Password(request.Password);

                var user = await _unitOfWork.UserRepository.FindByUsername(givenUsername, cancellationToken);

                var authentication = UserAuthentication.Authenticate(user, givenPassword, _passwordMatches);

                var token = _tokenGenerator.GenerateFor(user, authentication);

                authentication.Activate(token.ValidUntil);

                _unitOfWork.UserAuthenticationRepository.Add(authentication);

                await _unitOfWork.Save(cancellationToken);

                return AuthenticationResult.Success(token.Value, authentication.RefreshToken.Value, token.RefreshInterval);
            }
            catch (Exception ex)
            {
                return AuthenticationResult.Fail(ex.Message);
            }
        }
    }
}
