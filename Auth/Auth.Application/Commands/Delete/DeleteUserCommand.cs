using BuildingBlocks.Application.Commands;
using System;

namespace Auth.Application.Commands.Delete
{
    public class DeleteUserCommand : ICommand<DeleteUserResult>
    {
        public Guid Id { get; }
        public DeleteUserCommand(Guid id)
        {
            Id = id;
        }
    }
}
