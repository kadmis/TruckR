using Auth.Application.Commands;
using Auth.Application.Models.Results;
using Auth.Domain.Exceptions.UserExceptions;
using Auth.Domain.Persistence;
using BuildingBlocks.Application.Handlers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Application.Commands.Handlers
{
    public class ActivateUserCommandHandler : ICommandHandler<ActivateUserCommand, UserActivationResult>
    {
        private readonly IUnitOfWork _uow;

        public ActivateUserCommandHandler(IUnitOfWork uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<UserActivationResult> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _uow.UserRepository.FindById(request.UserId, cancellationToken);

                if (user == null)
                {
                    throw new UserDoesntExistException();
                }

                user.Activate(request.ActivationId);

                await _uow.Save(cancellationToken);

                return UserActivationResult.Success();
            }
            catch (Exception ex)
            {
                return UserActivationResult.Fail(ex.Message);
            }
        }
    }
}
