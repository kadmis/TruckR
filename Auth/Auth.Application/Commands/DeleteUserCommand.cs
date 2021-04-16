using Auth.Application.Models.Results;
using BuildingBlocks.Application.Commands;
using System;

namespace Auth.Application.Commands
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
