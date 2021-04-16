using Auth.Application.Models.Results;
using BuildingBlocks.Application.Commands;
using System;

namespace Auth.Application.Commands
{
    public class ResetPasswordCommand : ICommand<ResetPasswordResult>
    {
        public Guid Id { get; }

        public ResetPasswordCommand(Guid id)
        {
            Id = id;
        }
    }
}
