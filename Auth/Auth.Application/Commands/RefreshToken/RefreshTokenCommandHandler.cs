using Auth.Application.Commands.Authenticate;
using Auth.Domain.Data.Entities;
using Auth.Domain.Persistence;
using Auth.Domain.Services.TokenGeneration;
using BuildingBlocks.Application.Handlers;
using BuildingBlocks.Application.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Application.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : ICommandHandler<RefreshTokenCommand, RefreshTokenResult>
    {
        private readonly IIdentityAccessor _identity;
        private readonly IUnitOfWork _uow;
        private readonly ITokenGenerator _tokenGenerator;

        public RefreshTokenCommandHandler(
            IIdentityAccessor identity,
            IUnitOfWork uow,
            ITokenGenerator tokenGenerator)
        {
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _tokenGenerator = tokenGenerator ?? throw new ArgumentNullException(nameof(tokenGenerator));
        }

        public async Task<RefreshTokenResult> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var identity = _identity.UserIdentity();

                var user = await _uow.UserRepository.FindById(identity.UserId, cancellationToken);

                var authentication = await _uow.UserAuthenticationRepository.FindById(identity.AuthenticationId, cancellationToken);

                var newAuthentication = UserAuthentication.Refresh(user, authentication, request.RefreshToken);

                var token = _tokenGenerator.GenerateFor(user, newAuthentication);

                newAuthentication.Activate(token.ValidUntil);

                _uow.UserAuthenticationRepository.Add(newAuthentication);

                await _uow.Save(cancellationToken);

                return RefreshTokenResult.Success(token.Value, newAuthentication.RefreshToken.Value);
            }
            catch (Exception ex)
            {
                return RefreshTokenResult.Fail(ex.Message);
            }
        }
    }
}
