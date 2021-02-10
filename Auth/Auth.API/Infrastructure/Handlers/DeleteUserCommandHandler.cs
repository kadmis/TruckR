using Auth.API.Infrastructure.Commands;
using Auth.API.Infrastructure.Models.CommandsResults;
using Auth.Domain.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.API.Infrastructure.Handlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, UserDeletionResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDeletionResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.UserRepository.Delete(request.Id);
                await _unitOfWork.Save();

                return UserDeletionResult.Success(request.Id);
            }
            catch(Exception ex)
            {
                return UserDeletionResult.Fail(ex.Message);
            }
        }
    }
}
