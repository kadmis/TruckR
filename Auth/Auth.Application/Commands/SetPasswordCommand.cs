using Auth.Application.Models.Results;
using BuildingBlocks.Application.Commands;
using System;

namespace Auth.Application.Commands
{
    public class SetPasswordCommand : ICommand<SetPasswordResult>
    {
        public Guid UserId { get; }
        public Guid ResetToken { get; }
        public string Password { get; }

        public SetPasswordCommand(Guid userId, Guid resetToken, string password)
        {
            UserId = userId;
            ResetToken = resetToken;
            Password = password;
        }
    }
}
