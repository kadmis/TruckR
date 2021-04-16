using Auth.Application.Models.Results;
using BuildingBlocks.Application.Commands;
using System;

namespace Auth.Application.Commands
{
    public class ActivateUserCommand : ICommand<UserActivationResult>
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
