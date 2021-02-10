using Auth.API.Infrastructure.Models.CommandsResults;
using System;

namespace Auth.API.Infrastructure.Commands
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
