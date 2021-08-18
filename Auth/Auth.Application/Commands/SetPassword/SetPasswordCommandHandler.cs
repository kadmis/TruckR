using Auth.Application.Models.Results;
using Auth.Domain.Data.ValueObjects;
using Auth.Domain.Exceptions.UserExceptions;
using Auth.Domain.Persistence;
using BuildingBlocks.Application.Handlers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Application.Commands.SetPassword
{
    public class SetPasswordCommandHandler : ICommandHandler<SetPasswordCommand, SetPasswordResult>
    {
        private readonly IUnitOfWork _uow;

        public SetPasswordCommandHandler(IUnitOfWork uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<SetPasswordResult> Handle(SetPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _uow.UserRepository.FindById(request.UserId, cancellationToken);

                if (user == null)
                {
                    throw new UserDoesntExistException();
                }

                user.SetPassword(new Password(request.Password), request.ResetToken);
                await _uow.Save(cancellationToken);

                return SetPasswordResult.Success();
            }
            catch (Exception ex)
            {
                return SetPasswordResult.Fail(ex.Message);
            }
        }
    }
}
