using BuildingBlocks.Application.Commands;
using System;

namespace Auth.Application.Commands.Activate
{
    public class ActivateUserCommand : ICommand<ActivateUserResult>
    {
        public Guid UserId { get; }
        public Guid ActivationId { get; }

        public ActivateUserCommand(Guid userId, Guid activationId)
        {
            UserId = userId;
            ActivationId = activationId;
        }
    }
}
