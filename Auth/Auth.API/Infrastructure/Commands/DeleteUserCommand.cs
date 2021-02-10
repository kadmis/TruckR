using Auth.API.Infrastructure.Models.CommandsResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.API.Infrastructure.Commands
{
    public class DeleteUserCommand : ICommand<UserDeletionResult>
    {
        public Guid Id { get; }
        public DeleteUserCommand(Guid id)
        {
            Id = id;
        }
    }
}
