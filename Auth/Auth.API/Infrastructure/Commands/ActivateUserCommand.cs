using Auth.API.Infrastructure.Models.CommandsResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.API.Infrastructure.Commands
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
