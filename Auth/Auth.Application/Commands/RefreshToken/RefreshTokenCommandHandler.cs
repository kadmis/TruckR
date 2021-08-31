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
        private readonly IUnitOfWork _uow;
        private readonly ITokenGenerator _tokenGenerator;

        public RefreshTokenCommandHandler(
            IUnitOfWork uow,
            ITokenGenerator tokenGenerator)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _tokenGenerator = tokenGenerator ?? throw new ArgumentNullException(nameof(tokenGenerator));
        }

        public async Task<RefreshTokenResult> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _uow.UserRepository.FindById(request.UserId, cancellationToken);

                var authentication = await _uow.UserAuthenticationRepository.FindById(request.AuthenticationId, cancellationToken);

                var refreshed = UserAuthentication.Refresh(user, authentication, request.RefreshToken);

                var token = _tokenGenerator.GenerateFor(user, refreshed);

                refreshed.Activate(token.ValidUntil);

                if (refreshed.Id == authentication.Id)
                    _uow.UserAuthenticationRepository.Update(refreshed);
                else
                    _uow.UserAuthenticationRepository.Add(refreshed);

                await _uow.Save(cancellationToken);

                return RefreshTokenResult.Success(token.Value, refreshed.RefreshToken.Value, token.RefreshInterval);
            }
            catch (Exception ex)
            {
                return RefreshTokenResult.Fail(ex.Message);
            }
        }
    }
}
